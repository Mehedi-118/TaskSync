using System.Text.Json;

namespace PTSL.Ovidhan.Web.Core.Helper;

public static class SerializerOption
{
    public static JsonSerializerOptions Default = new()
    {
        PropertyNamingPolicy = null,
    };
}
