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
                var nonPrimitiveTypes = subtypes.Where(x => !x.IsPrimitive && x != typeof(string)).ToArray();
                var dicTypes = nonPrimitiveTypes
                    .Where(x => x.IsGenericType)
                    .Where(x => typeof(IDictionary<,>).IsAssignableFrom(x.GetGenericTypeDefinition()))
                    .ToArray();

                if (dicTypes.Length == 1)
                {
                    var jobj = JObject.Load(reader);
                    value =
                        Activator.CreateInstance(
                            typeof(Dictionary<,>).MakeGenericType(dicTypes[0].GetGenericArguments()));
                    serializer.Populate(jobj.CreateReader(), value);
                }
                else if (nonPrimitiveTypes.Length == 1)
                {
                    var jobj = JObject.Load(reader);
                    value = Activator.CreateInstance(nonPrimitiveTypes[0]);
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
                    value = serializer.Deserialize(reader, typeof(List<>).MakeGenericType(listTypes[0].GetGenericArguments()));
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
