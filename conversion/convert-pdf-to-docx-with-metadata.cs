using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";
        const string author = "John Doe";
        const string title = "Sample Document";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, set metadata, and save as DOCX using DocSaveOptions
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set custom metadata properties
            pdfDoc.Info.Author = author;
            pdfDoc.Info.Title = title;

            // Configure DOCX conversion options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX,          // Output as DOCX
                Mode = DocSaveOptions.RecognitionMode.Flow,     // Full recognition for editability
                RecognizeBullets = true                         // Enable bullet recognition
            };

            // Save the document as DOCX with the specified options
            pdfDoc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF '{inputPdf}' converted to DOCX '{outputDocx}' with author and title metadata.");
    }
}