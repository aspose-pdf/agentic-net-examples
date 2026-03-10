using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input_form.pdf";          // source PDF with a submit button
        const string submitUrl   = "https://example.com/submit"; // target URL for form submission

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF form using the Facade API
        using (Form form = new Form(inputPdfPath))
        {
            // OPTIONAL: list submit button names (for diagnostic purposes)
            string[] submitButtonNames = form.FormSubmitButtonNames;
            if (submitButtonNames != null && submitButtonNames.Length > 0)
            {
                Console.WriteLine("Submit button(s) found:");
                foreach (string name in submitButtonNames)
                    Console.WriteLine($" - {name}");
            }
            else
            {
                Console.WriteLine("No submit buttons detected in the PDF.");
            }

            // Export the current form field values to an FDF stream
            using (MemoryStream fdfStream = new MemoryStream())
            {
                form.ExportFdf(fdfStream);
                fdfStream.Position = 0; // reset for reading

                // Perform the HTTP POST to the target URL using the exported FDF data
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpContent httpContent = new StreamContent(fdfStream);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.fdf");

                    try
                    {
                        HttpResponseMessage response = httpClient.PostAsync(submitUrl, httpContent).Result;
                        Console.WriteLine($"Submission response: {(int)response.StatusCode} {response.ReasonPhrase}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error during form submission: {ex.Message}");
                    }
                }
            }
        }
    }
}