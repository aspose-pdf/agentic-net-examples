using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "unprotected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileSignature is the class that owns usage‑rights members.
            using (PdfFileSignature sig = new PdfFileSignature())
            {
                sig.BindPdf(inputPath);
                bool hasRightsBefore = sig.ContainsUsageRights();
                Console.WriteLine($"Contains usage rights before removal: {hasRightsBefore}");

                if (hasRightsBefore)
                {
                    sig.RemoveUsageRights();
                }

                // Save the (potentially) modified PDF.
                sig.Save(outputPath);
                Console.WriteLine($"Usage rights removed (if any), output saved to '{outputPath}'.");
            }

            // Verify removal by loading the output file again.
            using (PdfFileSignature verifySig = new PdfFileSignature())
            {
                verifySig.BindPdf(outputPath);
                bool hasRightsAfter = verifySig.ContainsUsageRights();
                Console.WriteLine($"Contains usage rights after removal: {hasRightsAfter}");

                if (!hasRightsAfter)
                    Console.WriteLine("Verification succeeded: usage rights have been removed.");
                else
                    Console.WriteLine("Verification failed: usage rights are still present.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
