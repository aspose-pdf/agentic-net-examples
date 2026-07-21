using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Extract all text fragments from the first page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[1].Accept(absorber);

            // Iterate over each extracted fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Try to invoke GetLineBreakInfo via reflection (method may not exist in all versions)
                var methodInfo = typeof(Aspose.Pdf.Text.TextFragment).GetMethod("GetLineBreakInfo");
                if (methodInfo != null)
                {
                    object lineBreakInfo = methodInfo.Invoke(fragment, null);
                    Console.WriteLine($"Fragment text: \"{fragment.Text}\"");
                    Console.WriteLine($"Line break info: {lineBreakInfo}");
                }
                else
                {
                    // Fallback: output basic fragment information if the method is unavailable
                    Console.WriteLine($"Fragment text: \"{fragment.Text}\"");
                    Console.WriteLine("GetLineBreakInfo method not available in this version of Aspose.Pdf.");
                }

                Console.WriteLine(); // blank line for readability
            }
        }
    }
}