using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachPath1 = "attachment1.txt";
        const string attachPath2 = "attachment2.jpg";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Verify that the attachment files exist
        if (!File.Exists(attachPath1) || !File.Exists(attachPath2))
        {
            Console.Error.WriteLine("One or both attachment files are missing.");
            return;
        }

        // Load the PDF using the Document API
        Document pdf = new Document(inputPdfPath);

        // ----- First attachment -------------------------------------------------
        var fileSpec1 = new FileSpecification(attachPath1, "First attachment description");
        fileSpec1.Contents = new MemoryStream(File.ReadAllBytes(attachPath1));
        pdf.EmbeddedFiles.Add(fileSpec1);

        // ----- Second attachment ------------------------------------------------
        var fileSpec2 = new FileSpecification(attachPath2, "Second attachment description");
        fileSpec2.Contents = new MemoryStream(File.ReadAllBytes(attachPath2));
        pdf.EmbeddedFiles.Add(fileSpec2);

        // Save the modified PDF containing the attachments
        pdf.Save(outputPdfPath);

        Console.WriteLine($"PDF saved with attachments to '{outputPdfPath}'.");
    }
}