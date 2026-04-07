using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfGenerator
{
    // Generates a PDF from an existing file with retry logic for transient I/O errors.
    // maxRetries: number of attempts (initial try + retries)
    // delayMs: wait time between retries
    public static void GeneratePdfWithRetry(string inputPath, string outputPath, int maxRetries = 3, int delayMs = 1000)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        int attempt = 0;
        while (true)
        {
            attempt++;
            try
            {
                // Load the source PDF (lifecycle rule: use Document constructor)
                using (Document doc = new Document(inputPath))
                {
                    // Example processing: add a simple text fragment on the first page
                    // (demonstrates that some work is done before saving)
                    if (doc.Pages.Count > 0)
                    {
                        Page firstPage = doc.Pages[1]; // page indexing is 1‑based
                        TextFragment tf = new TextFragment("Generated on attempt " + attempt);
                        tf.Position = new Position(100, 700);
                        firstPage.Paragraphs.Add(tf);
                    }

                    // Save the resulting PDF (lifecycle rule: use Document.Save)
                    doc.Save(outputPath);
                }

                // If we reach here, the operation succeeded
                Console.WriteLine($"PDF generated successfully on attempt {attempt}.");
                break;
            }
            catch (PdfException ex) // transient I/O errors are reported as PdfException
            {
                Console.Error.WriteLine($"PdfException on attempt {attempt}: {ex.Message}");

                if (attempt >= maxRetries)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Operation failed.");
                    throw; // re‑throw to let the caller handle the failure
                }

                // Wait before retrying (simple back‑off)
                Thread.Sleep(delayMs);
                Console.WriteLine($"Retrying... (attempt {attempt + 1} of {maxRetries})");
            }
            catch (IOException ex) // other I/O related exceptions
            {
                Console.Error.WriteLine($"IOException on attempt {attempt}: {ex.Message}");

                if (attempt >= maxRetries)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Operation failed.");
                    throw;
                }

                Thread.Sleep(delayMs);
                Console.WriteLine($"Retrying... (attempt {attempt + 1} of {maxRetries})");
            }
        }
    }

    // Example entry point
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_generated.pdf";

        try
        {
            GeneratePdfWithRetry(inputPdf, outputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Final failure: {ex.Message}");
        }
    }
}