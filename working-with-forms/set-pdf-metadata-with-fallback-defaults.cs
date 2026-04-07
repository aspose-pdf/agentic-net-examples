using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPdfPath = "output_with_defaults.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdf = new Document(pdfPath))
        {
            // -----------------------------------------------------------------
            // Define fallback default values for the standard metadata fields we
            // expect. The keys correspond to the Document.Info property names.
            // -----------------------------------------------------------------
            var defaultValues = new Dictionary<string, string>
            {
                { "Creator",      "Unknown Creator" },
                { "Title",        "Untitled Document" },
                { "Description",  "No description provided." }
            };

            // Apply defaults only when the corresponding property is missing or empty.
            if (string.IsNullOrWhiteSpace(pdf.Info.Author))
                pdf.Info.Author = defaultValues["Creator"];

            if (string.IsNullOrWhiteSpace(pdf.Info.Title))
                pdf.Info.Title = defaultValues["Title"];

            if (string.IsNullOrWhiteSpace(pdf.Info.Subject))
                pdf.Info.Subject = defaultValues["Description"];

            // Save the modified PDF.
            pdf.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with fallback values: {outputPdfPath}");
    }
}