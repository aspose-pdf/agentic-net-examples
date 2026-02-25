using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file you want to open
        string inputFile = "sample.pdf";

        // Verify that the file exists to avoid FileNotFoundException
        if (!File.Exists(inputFile))
        {
            Console.WriteLine($"Error: File not found -> {Path.GetFullPath(inputFile)}");
            return;
        }

        // Load the PDF document using the Aspose.Pdf.Document constructor
        Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputFile);

        // Example operation: output the number of pages in the document
        Console.WriteLine($"PDF loaded successfully. Page count: {pdfDocument.Pages.Count}");

        // (Optional) Save a copy of the opened document to demonstrate saving
        string outputFile = "copy.pdf";
        pdfDocument.Save(outputFile);
        Console.WriteLine($"Document saved as: {Path.GetFullPath(outputFile)}");
    }
}