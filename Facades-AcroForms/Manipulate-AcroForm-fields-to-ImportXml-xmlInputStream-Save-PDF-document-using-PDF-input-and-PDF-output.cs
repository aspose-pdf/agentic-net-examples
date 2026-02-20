using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, input XML path, output PDF path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputPdf> <inputXml> <outputPdf>");
            return;
        }

        string pdfPath = args[0];
        string xmlPath = args[1];
        string outputPath = args[2];

        // Validate file existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Bind the document to the Form facade
                using (Form form = new Form(pdfDocument))
                {
                    // Open the XML stream and import data into the form fields
                    using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                    {
                        form.ImportXml(xmlStream);
                    }

                    // Save the modified PDF
                    pdfDocument.Save(outputPath);
                }
            }

            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}