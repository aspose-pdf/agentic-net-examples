using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF and create a PdfFileInfo instance for metadata operations
        PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

        // Read existing metadata
        Console.WriteLine($"Title   : {pdfInfo.Title}");
        Console.WriteLine($"Author  : {pdfInfo.Author}");
        Console.WriteLine($"Subject : {pdfInfo.Subject}");
        Console.WriteLine($"Keywords: {pdfInfo.Keywords}");

        // Example: modify metadata
        pdfInfo.Title  = "Updated Document Title";
        pdfInfo.Author = "Jane Doe";

        // Save the updated metadata back to the PDF file
        pdfInfo.Save(pdfPath);

        Console.WriteLine("Metadata has been updated successfully.");
    }
}