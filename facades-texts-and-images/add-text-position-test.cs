using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string searchText = "Sample Text";
        const double expectedX = 100.0;
        const double expectedY = 500.0;

        // Create a PDF and add text at the specified coordinates
        using (Document document = new Document())
        {
            Page page = document.Pages.Add();
            TextFragment textFragment = new TextFragment(searchText);
            textFragment.Position = new Position(expectedX, expectedY);
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(textFragment);
            document.Save(outputPath);
        }

        // Verify that the added text appears at the expected coordinates
        using (Document document = new Document(outputPath))
        {
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            document.Pages.Accept(absorber);
            if (absorber.TextFragments.Count > 0)
            {
                TextFragment foundFragment = absorber.TextFragments[1];
                double actualX = foundFragment.Position.XIndent;
                double actualY = foundFragment.Position.YIndent;
                bool xMatch = Math.Abs(actualX - expectedX) < 0.1;
                bool yMatch = Math.Abs(actualY - expectedY) < 0.1;
                if (xMatch && yMatch)
                {
                    Console.WriteLine("PASS: Text position matches expected coordinates.");
                }
                else
                {
                    Console.WriteLine($"FAIL: Expected ({expectedX},{expectedY}) but got ({actualX},{actualY}).");
                }
            }
            else
            {
                Console.WriteLine("FAIL: Text not found in the document.");
            }
        }
    }
}
