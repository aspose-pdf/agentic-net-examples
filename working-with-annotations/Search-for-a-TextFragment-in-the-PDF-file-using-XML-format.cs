using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputXml = "output.xml";
        const string searchPhrase = "sample";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Create a TextFragmentAbsorber to search for the specified phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
                // Perform the search on the whole document
                doc.Pages.Accept(absorber);

                // Output found fragments to the console
                int idx = 1;
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"Fragment {idx}: Text=\"{fragment.Text}\" Page={fragment.Page.Number}");
                    idx++;
                }

                // Save the PDF as XML using explicit XmlSaveOptions
                XmlSaveOptions xmlOpts = new XmlSaveOptions();
                doc.Save(outputXml, xmlOpts);
            }

            Console.WriteLine($"Search completed. XML saved to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}