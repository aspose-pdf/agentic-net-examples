using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "sample.pdf";

        // Page numbers to query (including some that may not exist)
        List<int> pagesToCheck = new List<int> { 1, 2, 0, 5, 10 };

        // Ensure the PDF file exists before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Use PdfPageEditor (facade) to query page sizes
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the editor to the loaded document
                editor.BindPdf(doc);

                // Iterate over the requested page numbers
                foreach (int pageNumber in pagesToCheck)
                {
                    try
                    {
                        // GetPageSize expects 1‑based page numbers.
                        // If the page does not exist, an exception will be thrown.
                        PageSize size = editor.GetPageSize(pageNumber);
                        Console.WriteLine($"Page {pageNumber}: Width = {size.Width} pt, Height = {size.Height} pt");
                    }
                    catch (Exception ex)
                    {
                        // Handle non‑existent page numbers gracefully
                        Console.WriteLine($"Unable to retrieve size for page {pageNumber}: {ex.Message}");
                    }
                }
            }
        }
    }
}