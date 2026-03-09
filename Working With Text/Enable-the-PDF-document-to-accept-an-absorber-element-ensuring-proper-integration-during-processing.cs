using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Create an absorber element – here a TextAbsorber that extracts text
            // -----------------------------------------------------------------
            TextAbsorber textAbsorber = new TextAbsorber();

            // -----------------------------------------------------------------
            // Integrate the absorber with the document by calling Accept on the
            // PageCollection. This invokes the appropriate overload:
            // PageCollection.Accept(TextAbsorber)
            // -----------------------------------------------------------------
            doc.Pages.Accept(textAbsorber);

            // Example usage of the absorber's result (optional)
            Console.WriteLine($"Extracted text length: {textAbsorber.Text?.Length ?? 0}");

            // Save the (potentially modified) document.
            // No SaveOptions are needed because we are saving as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}