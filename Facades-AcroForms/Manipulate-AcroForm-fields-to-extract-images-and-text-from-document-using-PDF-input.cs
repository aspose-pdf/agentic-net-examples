using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // required for Resolution struct

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output directory (second argument)
        string pdfPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputDir = args.Length > 1 ? args[1] : "output";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // ------------------------------------------------------------
            // Extract text fields using the Form facade (AcroForm handling)
            // ------------------------------------------------------------
            using (Form form = new Form())
            {
                // Bind the PDF document to the Form facade
                form.BindPdf(pdfPath);

                Console.WriteLine("Extracted text fields:");
                // Iterate over all field names in the form
                foreach (string fieldName in form.FieldNames)
                {
                    // Get the field type as an enum (FieldType)
                    var fieldType = form.GetFieldType(fieldName);
                    if (fieldType == FieldType.Text)
                    {
                        // Retrieve the field value; it may be null
                        string value = form.GetField(fieldName)?.ToString() ?? string.Empty;
                        Console.WriteLine($"{fieldName}: {value}");

                        // Save each text field value to a separate .txt file
                        string textFilePath = Path.Combine(outputDir, $"{fieldName}.txt");
                        File.WriteAllText(textFilePath, value);
                    }
                }
            }

            // ------------------------------------------------------------
            // Extract all images from the PDF using PdfConverter
            // ------------------------------------------------------------
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the same PDF document to the converter
                converter.BindPdf(pdfPath);

                // Convert all pages (1‑based indexing)
                converter.StartPage = 1;
                converter.EndPage = converter.PageCount;

                // Set a reasonable resolution (150 DPI)
                converter.Resolution = new Resolution(150);

                // Prepare the converter
                converter.DoConvert();

                int imageIndex = 1;
                // Loop while there are more images to retrieve
                while (converter.HasNextImage())
                {
                    // Save each extracted image as JPEG
                    string imagePath = Path.Combine(outputDir, $"Image_{imageIndex}.jpg");
                    converter.GetNextImage(imagePath);
                    imageIndex++;
                }
            }

            Console.WriteLine("Extraction of text fields and images completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
