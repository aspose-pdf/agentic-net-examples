using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load PDF meta‑information using the Facade class.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Example modification – set a new title.
            pdfInfo.Title = "Updated Document Title";

            // Attempt to save the updated information.
            try
            {
                bool saved = pdfInfo.SaveNewInfo(outputPdf);
                if (!saved)
                {
                    Console.Error.WriteLine("SaveNewInfo reported failure without throwing.");
                }
                else
                {
                    Console.WriteLine($"Metadata saved to '{outputPdf}'.");
                }
            }
            catch (IOException ioEx) when (IsReadOnly(outputPdf))
            {
                // The target file is read‑only. Remove the attribute and retry.
                Console.WriteLine("Target file is read‑only. Removing attribute and retrying.");

                RemoveReadOnlyAttribute(outputPdf);

                // Retry the save operation.
                bool retrySaved = pdfInfo.SaveNewInfo(outputPdf);
                if (retrySaved)
                {
                    Console.WriteLine($"Metadata saved to '{outputPdf}' after clearing read‑only flag.");
                }
                else
                {
                    Console.Error.WriteLine("Retry of SaveNewInfo failed.");
                }
            }
            catch (Exception ex)
            {
                // Any other exception – report and exit.
                Console.Error.WriteLine($"Error during SaveNewInfo: {ex.Message}");
            }
        }
    }

    // Helper: determines whether the specified file exists and has the ReadOnly attribute.
    private static bool IsReadOnly(string path)
    {
        return File.Exists(path) && (File.GetAttributes(path) & FileAttributes.ReadOnly) != 0;
    }

    // Helper: clears the ReadOnly attribute from the file.
    private static void RemoveReadOnlyAttribute(string path)
    {
        if (File.Exists(path))
        {
            FileAttributes attrs = File.GetAttributes(path);
            attrs &= ~FileAttributes.ReadOnly;
            File.SetAttributes(path, attrs);
        }
    }
}