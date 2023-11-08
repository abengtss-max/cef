using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("https://akaa-baseurl-xxxxxxxxxxx-xxxxxxxxxxxxx.luna.akamaiapis.net/siem/v1/configs");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            // Parse the CEF logs from the responseBody
            var match = Regex.Match(responseBody, @"CEF:(?<Version>\d+)\|(?<DeviceVendor>[^|]*)\|(?<DeviceProduct>[^|]*)\|(?<DeviceVersion>[^|]*)\|(?<SignatureID>[^|]*)\|(?<Name>[^|]*)\|(?<Severity>[^|]*)\|(?<Extension>.*)");
            if (match.Success)
            {
                Console.WriteLine($"Version: {match.Groups["Version"].Value}");
                Console.WriteLine($"DeviceVendor: {match.Groups["DeviceVendor"].Value}");
                Console.WriteLine($"DeviceProduct: {match.Groups["DeviceProduct"].Value}");
                Console.WriteLine($"DeviceVersion: {match.Groups["DeviceVersion"].Value}");
                Console.WriteLine($"SignatureID: {match.Groups["SignatureID"].Value}");
                Console.WriteLine($"Name: {match.Groups["Name"].Value}");
                Console.WriteLine($"Severity: {match.Groups["Severity"].Value}");
                Console.WriteLine($"Extension: {match.Groups["Extension"].Value}");
            }
        }
        catch(HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ",e.Message);
        }
    }
}