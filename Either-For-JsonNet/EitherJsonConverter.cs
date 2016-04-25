using BCL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Either_For_JsonNet
{
    public class EitherJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEither).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var either = (IEither)value;
            var eitherValue = either.Value;
            serializer.Serialize(writer, eitherValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var subtypes = objectType.GetGenericArguments();
            var value = reader.Value;
            if (reader.TokenType == JsonToken.Integer)
            {
                var largestIntType = subtypes.WithMax(NumericTypesHelper.GetIntegerSize);
                value = Convert.ChangeType(value, largestIntType);
            }
            else if (reader.TokenType == JsonToken.Float)
            {
                var largestIntType = subtypes.WithMax(NumericTypesHelper.GetFloatSize);
                value = Convert.ChangeType(value, largestIntType);
            }
            else if (reader.TokenType == JsonToken.String)
            {
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                var nonPrimitiveNorCollectionTypes = subtypes
                    .Where(x => !x.IsPrimitive && x != typeof(string))
                    .Where(x => !x.IsGenericType || !typeof(IEnumerable<>).IsAssignableFrom(x.GetGenericTypeDefinition()))
                    .ToArray();

                var dictionaryTypes = subtypes
                    .Where(x => x.IsGenericType)
                    .Where(x => typeof(IDictionary<,>).IsAssignableFrom(x.GetGenericTypeDefinition()))
                    .ToArray();

                if (nonPrimitiveNorCollectionTypes.Length == 1)
                {
                    var jobj = JObject.Load(reader);
                    value = Activator.CreateInstance(nonPrimitiveNorCollectionTypes[0]);
                    serializer.Populate(jobj.CreateReader(), value);
                }
                else if (dictionaryTypes.Length == 1)
                {
                    var dictionaryType = dictionaryTypes[0].GetGenericTypeDefinition() == typeof(IDictionary<,>)
                        ? typeof(Dictionary<,>).MakeGenericType(dictionaryTypes[0].GetGenericArguments())
                        : dictionaryTypes[0];

                    value = Activator.CreateInstance(dictionaryType);
                    var jobj = JObject.Load(reader);
                    serializer.Populate(jobj.CreateReader(), value);
                }
                else
                    throw new InvalidOperationException("Cannot decide between types to deserialize.");
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                var listTypes = (
                    from x in subtypes
                    where x.IsGenericType
                    let def = x.GetGenericTypeDefinition()
                    where typeof(ICollection<>).IsAssignableFrom(def) || typeof(IList<>).IsAssignableFrom(def)
                    where !typeof(IDictionary<,>).IsAssignableFrom(def)
                    select x
                    ).ToArray();

                if (listTypes.Length == 1)
                {
                    value = serializer.Deserialize(reader, listTypes[0]);
                }
                else
                    throw new InvalidOperationException("Cannot decide between types to deserialize.");
            }
            else if (reader.TokenType == JsonToken.Null)
            {
            }
            else
                throw new InvalidOperationException("Cannot deserialize JSON to the `Either` type.");

            var either = Either.Factory.Create(value, objectType);

            return either;
        }
    }
}
