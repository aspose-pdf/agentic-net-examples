using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        try
        {
            // Verify that the source file exists before attempting to open it.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // PdfFileMend is a Facade that allows editing of an existing PDF.
            // It implements IDisposable, so we wrap it in a using block.
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the PDF file to the facade.
                mend.BindPdf(inputPath);

                // Access the underlying Document for manipulation.
                Document doc = mend.Document;

                // Simple operation: add a text fragment to the first page.
                Page page = doc.Pages[1]; // Pages are 1‑based.
                TextFragment tf = new TextFragment("Hello Aspose!");
                tf.Position = new Position(100, 700); // Position uses Aspose.Pdf.Text.Position.
                page.Paragraphs.Add(tf);

                // Save the modified PDF via the facade.
                mend.Save(outputPath);
            }

            Console.WriteLine($"PDF processed and saved to '{outputPath}'.");
        }
        // Catch file‑system related errors (e.g., missing file, permission issues).
        catch (IOException ioEx)
        {
            Console.Error.WriteLine($"I/O error: {ioEx.Message}");
        }
        // Catch errors specific to Aspose.Pdf operations.
        catch (PdfException pdfEx)
        {
            Console.Error.WriteLine($"PDF error: {pdfEx.Message}");
        }
        // Catch any other unexpected exceptions.
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}