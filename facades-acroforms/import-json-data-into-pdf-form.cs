using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form_template.pdf";
        const string jsonData = "data.json";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(jsonData))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonData}");
            return;
        }

        try
        {
            // Use the Facade Form class (Aspose.Pdf.Facades.Form) which provides ImportJson.
            using (var form = new Aspose.Pdf.Facades.Form())
            {
                // Bind the PDF that contains the form fields.
                form.BindPdf(inputPdf);

                // Import field values from the JSON file via a stream.
                using (FileStream jsonStream = File.OpenRead(jsonData))
                {
                    form.ImportJson(jsonStream);
                }

                // Save the updated PDF.
                form.Save(outputPdf);
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdf}'.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
