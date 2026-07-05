using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFile  = "input.pdf";
        const string outputFile = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputFile))
        {
            Console.Error.WriteLine($"Input file not found: {inputFile}");
            return;
        }

        // If the output file already exists and is read‑only, clear the attribute
        if (File.Exists(outputFile))
        {
            FileAttributes attrs = File.GetAttributes(outputFile);
            if ((attrs & FileAttributes.ReadOnly) != 0)
            {
                File.SetAttributes(outputFile, attrs & ~FileAttributes.ReadOnly);
            }
        }

        try
        {
            // Load the PDF file information using the Facade API
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputFile))
            {
                // Example: modify some metadata (optional)
                pdfInfo.Title  = "Updated Title";
                pdfInfo.Author = "John Doe";

                // Attempt to save the updated information
                bool saved = pdfInfo.SaveNewInfo(outputFile);

                if (saved)
                {
                    Console.WriteLine($"Metadata saved successfully to '{outputFile}'.");
                }
                else
                {
                    Console.Error.WriteLine("SaveNewInfo returned false – operation failed.");
                }
            }
        }
        catch (IOException ioEx)
        {
            // Handle read‑only file scenario or other I/O errors
            Console.Error.WriteLine($"I/O error: {ioEx.Message}");

            // If the error is due to a read‑only attribute, try to clear it and retry once
            if ((File.GetAttributes(outputFile) & FileAttributes.ReadOnly) != 0)
            {
                try
                {
                    File.SetAttributes(outputFile, File.GetAttributes(outputFile) & ~FileAttributes.ReadOnly);
                    // Retry the save operation
                    using (PdfFileInfo retryInfo = new PdfFileInfo(inputFile))
                    {
                        retryInfo.Title  = "Updated Title";
                        retryInfo.Author = "John Doe";
                        if (retryInfo.SaveNewInfo(outputFile))
                        {
                            Console.WriteLine($"Metadata saved successfully after clearing read‑only flag to '{outputFile}'.");
                        }
                        else
                        {
                            Console.Error.WriteLine("Retry SaveNewInfo returned false – operation failed.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Retry failed: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // General exception handling for any other errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}