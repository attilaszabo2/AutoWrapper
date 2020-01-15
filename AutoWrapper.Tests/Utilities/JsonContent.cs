using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace AutoWrapper.WebAPI.Tests.Utilities
{
    public class JsonContent : StringContent
	{
		public JsonContent(object obj) : base(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json")
		{
		}
	}
}
