using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string secondPdf = "second.pdf";
        const string mergedPath = "merged.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("Required PDF files are missing.");
            return;
        }

        // ------------------------------------------------------------
        // Example 1: Extract text from a PDF using PdfExtractor (Facade)
        // ------------------------------------------------------------
        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Bind the document to the extractor facade
                PdfExtractor extractor = new PdfExtractor();
                extractor.BindPdf(doc);

                // Extract text from the whole document
                extractor.ExtractText();

                // Save the extracted text to a .txt file
                string textPath = Path.ChangeExtension(outputPath, ".txt");
                extractor.GetText(textPath);
                Console.WriteLine($"Extracted text saved to '{textPath}'.");

                // Release resources held by the facade
                extractor.Close();
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (InvalidPdfFileFormatException ex)
        {
            Console.Error.WriteLine($"Invalid PDF format: {ex.Message}");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"General PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }

        // ------------------------------------------------------------
        // Example 2: Concatenate two PDFs using PdfFileEditor
        // ------------------------------------------------------------
        try
        {
            PdfFileEditor editor = new PdfFileEditor();

            // TryConcatenate returns false on failure; inspect LastException for details
            bool success = editor.TryConcatenate(inputPath, secondPdf, mergedPath);
            if (!success)
            {
                Console.Error.WriteLine("Concatenation failed.");
                if (editor.LastException != null)
                {
                    Console.Error.WriteLine($"Reason: {editor.LastException.Message}");
                }
            }
            else
            {
                Console.WriteLine($"PDFs concatenated successfully to '{mergedPath}'.");
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF error during concatenation: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during concatenation: {ex.Message}");
        }

        // ------------------------------------------------------------
        // Example 3: Open a PDF with PdfViewer and handle possible errors
        // ------------------------------------------------------------
        PdfViewer viewer = null;
        try
        {
            viewer = new PdfViewer();
            viewer.BindPdf(inputPath);

            // Perform a simple operation (printing). May throw on non‑Windows platforms.
            viewer.PrintDocument();
            Console.WriteLine("Print operation completed.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF error in viewer: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected viewer error: {ex.Message}");
        }
        finally
        {
            // Ensure the viewer releases the bound document
            if (viewer != null)
            {
                viewer.Close();
            }
        }
    }
}