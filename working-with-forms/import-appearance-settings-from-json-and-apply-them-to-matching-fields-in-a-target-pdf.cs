using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF with form fields
        const string jsonPath      = "appearance.json"; // JSON containing field values / appearance data
        const string outputPdfPath = "output.pdf";     // PDF after applying JSON data

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Bind the document to a Form facade
                using (Form form = new Form(pdfDoc))
                {
                    // Import field values (and any appearance settings supported by JSON) from the JSON file
                    using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
                    {
                        form.ImportJson(jsonStream);
                    }

                    // Save the modified document
                    pdfDoc.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"PDF saved with imported appearance settings: '{outputPdfPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}