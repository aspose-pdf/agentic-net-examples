using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace is required by the task

class Program
{
    // Entry point: args[0]=input PDF, args[1]=output PDF, args[2]=operation ("add" or "delete")
    // Optional args for "add": prefix, startNumber, numberOfDigits
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <outputPdf> <add|delete> [prefix] [startNumber] [digits]");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        string operation  = args[2].ToLowerInvariant();

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // The PageCollectionExtensions methods operate on the Pages collection
                if (operation == "add")
                {
                    // Optional parameters with sensible defaults
                    string prefix        = args.Length > 3 ? args[3] : string.Empty;
                    int    startNumber   = args.Length > 4 && int.TryParse(args[4], out int sn) ? sn : 1;
                    int    numberOfDigits = args.Length > 5 && int.TryParse(args[5], out int nd) ? nd : 6;

                    // Configure and add Bates numbering to every page
                    doc.Pages.AddBatesNumbering(artifact =>
                    {
                        artifact.Prefix          = prefix;          // e.g., "ABC-"
                        artifact.StartNumber     = startNumber;     // first Bates number
                        artifact.NumberOfDigits  = numberOfDigits;  // e.g., 6 -> 000001
                        // Position can be left to defaults (bottom margin, right alignment)
                        // If a specific position is required, set artifact.Position here:
                        // artifact.Position = new Aspose.Pdf.Rectangle(left, bottom, right, top);
                    });
                }
                else if (operation == "delete")
                {
                    // Remove all Bates numbering artifacts from every page
                    doc.Pages.DeleteBatesNumbering();
                }
                else
                {
                    Console.Error.WriteLine("Invalid operation. Use \"add\" or \"delete\".");
                    return;
                }

                // Save the modified document. No SaveOptions are needed because we keep PDF format.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Operation \"{operation}\" completed. Output saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}