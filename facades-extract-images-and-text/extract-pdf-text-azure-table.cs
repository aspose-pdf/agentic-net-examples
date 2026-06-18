using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document createDoc = new Document())
        {
            Page page = createDoc.Pages.Add();
            TextFragment tf = new TextFragment("Hello Aspose PDF!");
            page.Paragraphs.Add(tf);
            createDoc.Save("input.pdf");
        }

        // Load the PDF and extract its text
        string extractedText;
        using (Document loadDoc = new Document("input.pdf"))
        {
            TextAbsorber absorber = new TextAbsorber();
            loadDoc.Pages.Accept(absorber);
            extractedText = absorber.Text;
        }

        // Build a JSON payload representing an Azure Table entity
        string partitionKey = Guid.NewGuid().ToString();
        string rowKey = DateTime.UtcNow.Ticks.ToString();
        string jsonPayload = "{\"PartitionKey\":\"" + partitionKey + "\",\"RowKey\":\"" + rowKey + "\",\"Content\":\"" + EscapeJson(extractedText) + "\"}";

        // Send the payload to Azure Table Storage via REST (placeholder – authentication omitted)
        using (HttpClient client = new HttpClient())
        {
            // TODO: replace with your actual storage account name and table name
            string storageAccount = "youraccount";
            string tableName = "PdfTexts";
            string requestUri = "https://" + storageAccount + ".table.core.windows.net/" + tableName;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            // In a real scenario you must add the Authorization header with the Shared Key signature.
            HttpResponseMessage response = client.PostAsync(requestUri, content).Result;
            Console.WriteLine("Response status: " + response.StatusCode);
        }
    }

    static string EscapeJson(string s)
    {
        if (s == null)
        {
            return "";
        }
        return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
    }
}