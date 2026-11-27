using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 14 pages (1‑based indexing)
            if (pdfDoc.Pages.Count < 14)
            {
                Console.Error.WriteLine("The document does not contain page 14.");
                return;
            }

            // Create a TextFragmentAbsorber to capture individual text fragments on page 14
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Apply the absorber to page 14
            pdfDoc.Pages[14].Accept(absorber);

            // Reduce each fragment's font size by 12% (i.e., keep 88% of original)
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Guard against null TextState (should not happen)
                if (fragment.TextState != null && fragment.TextState.FontSize > 0)
                {
                    // FontSize is a float; use a float literal to avoid CS0266
                    fragment.TextState.FontSize *= 0.88f; // 12% reduction
                }
            }

            // After modifying the fragments, the changes are reflected in the document.
            // Extract the modified text by concatenating all fragment texts.
            string modifiedText = string.Empty;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                modifiedText += fragment.Text;
            }

            // Save the modified PDF if needed (optional)
            // pdfDoc.Save("modified.pdf");

            // Write the extracted, modified text to the output text file
            File.WriteAllText(outputTxtPath, modifiedText);
        }

        Console.WriteLine($"Modified text from page 14 saved to '{outputTxtPath}'.");
    }
}
