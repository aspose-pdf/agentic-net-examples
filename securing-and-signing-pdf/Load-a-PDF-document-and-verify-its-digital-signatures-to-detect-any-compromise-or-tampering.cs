using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Signatures;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create the compromise detector for the loaded document
            SignaturesCompromiseDetector detector = new SignaturesCompromiseDetector(doc);

            // Perform the check; the method returns true if no compromise is detected
            bool isClean = detector.Check(out CompromiseCheckResult checkResult);

            // Output the results
            Console.WriteLine($"Compromise detection succeeded: {isClean}");
            Console.WriteLine($"Has compromised signatures: {checkResult.HasCompromisedSignatures}");
            Console.WriteLine($"Signatures coverage state: {checkResult.SignaturesCoverage}");
        }
    }
}