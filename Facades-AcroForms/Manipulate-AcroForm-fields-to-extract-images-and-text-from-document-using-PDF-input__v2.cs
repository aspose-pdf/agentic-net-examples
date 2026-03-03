using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Extracted";
        const string textOutput = "extracted_text.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // ---------- AcroForm manipulation ----------
        // Load the PDF with the Form facade
        using (Form form = new Form(inputPdf))
        {
            // List all AcroForm field names and their current values
            Console.WriteLine("AcroForm fields:");
            foreach (string fieldName in form.FieldNames)
            {
                string value = form.GetField(fieldName);
                Console.WriteLine($"{fieldName} = {value}");
            }

            // Example: fill a text field named "FirstName" if it exists
            if (Array.Exists(form.FieldNames, f => f.Equals("FirstName", StringComparison.OrdinalIgnoreCase)))
            {
                form.FillField("FirstName", "John");
            }

            // Save the modified PDF to a new file
            string filledPdfPath = Path.Combine(outputFolder, "filled.pdf");
            form.Save(filledPdfPath);
            Console.WriteLine($"Form-filled PDF saved to: {filledPdfPath}");
        }

        // ---------- Image and text extraction ----------
        // Use PdfExtractor to pull out images and text from the same PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // ---- Extract images ----
            extractor.ExtractImage();
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputFolder, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Extracted image {imageIndex} to: {imagePath}");
                imageIndex++;
            }

            // ---- Extract text ----
            extractor.ExtractText();
            extractor.GetText(textOutput);
            Console.WriteLine($"Extracted text saved to: {textOutput}");
        }
    }
}