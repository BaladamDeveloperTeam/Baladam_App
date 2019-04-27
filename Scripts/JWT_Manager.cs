using System.Collections;
using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using JWT.Builder;


public class JWT_Manager
{

    private const string secret = "4C8kum4LxyKWYLM78sKdXrzbB8DCPTfX";

    public static string DecodeJWT(string token)
    {
        IJsonSerializer serializer = new JsonNetSerializer();
        IDateTimeProvider provider = new UtcDateTimeProvider();
        IJwtValidator validator = new JwtValidator(serializer, provider);
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
        string json = decoder.Decode(token, secret, verify: true);

        return json;
    }

    public static string EncodeToJWT(int expTime, string username, string pwd)
    {
        var token = new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(secret)
            .AddClaim("exp", System.DateTimeOffset.UtcNow.AddMinutes(expTime).ToUnixTimeSeconds())
            .AddClaim("username", username)
            .AddClaim("pwd", pwd)
            .Build();

        return token;
    }

    public static string EncodeToJWT(Dictionary<string, object> payload)
    {
        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        IJsonSerializer serializer = new JsonNetSerializer();
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
        string token = encoder.Encode(payload, secret);

        return token;
    }
}