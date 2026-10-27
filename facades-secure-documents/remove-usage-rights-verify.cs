using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_usage_rights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Usage‑rights operations are performed via PdfFileSignature, not via
            // PdfFileEditor. The signature object must be bound to a PDF before the
            // ContainsUsageRights / RemoveUsageRights methods can be used.
            // -----------------------------------------------------------------
            bool hasRightsBefore;
            using (PdfFileSignature signature = new PdfFileSignature())
            {
                signature.BindPdf(inputPath);
                hasRightsBefore = signature.ContainsUsageRights();
                Console.WriteLine($"Contains usage rights before removal: {hasRightsBefore}");

                // Remove the rights (if any) and save the result.
                if (hasRightsBefore)
                {
                    signature.RemoveUsageRights();
                }
                // Save creates a new file; if there were no rights we still copy the
                // original content to the output path so the rest of the program works
                // uniformly.
                signature.Save(outputPath);
            }

            // Verify that the output PDF no longer contains usage rights.
            bool hasRightsAfter;
            using (PdfFileSignature signature = new PdfFileSignature())
            {
                signature.BindPdf(outputPath);
                hasRightsAfter = signature.ContainsUsageRights();
            }
            Console.WriteLine($"Contains usage rights after removal: {hasRightsAfter}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
