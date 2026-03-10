using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PCL file (PCL is supported only as an input format)
        const string inputPclPath = "input.pcl";
        // Output PDF file (the document will be saved as PDF)
        const string outputPdfPath = "output.pdf";
        // Text to search for
        const string searchPhrase = "Sample Text";

        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"File not found: {inputPclPath}");
            return;
        }

        try
        {
            // Load the PCL file using PclLoadOptions
            using (Document doc = new Document(inputPclPath, new PclLoadOptions()))
            {
                // Create a TextFragmentAbsorber to search for the phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

                // Perform the search on the whole document
                absorber.Visit(doc);

                // Output the number of occurrences found
                Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{searchPhrase}\".");

                // Iterate through each found TextFragment and display its text and page number
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The Page property gives the page containing the fragment
                    Console.WriteLine($"Page {fragment.Page.Number}: \"{fragment.Text}\"");
                }

                // Save the loaded document as PDF (PCL cannot be saved)
                doc.Save(outputPdfPath);
                Console.WriteLine($"Document saved as PDF to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}