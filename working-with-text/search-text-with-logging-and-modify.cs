using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "search_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Enable logging of text extraction errors
            TextSearchOptions searchOptions = new TextSearchOptions(true)
            {
                LogTextExtractionErrors = true
            };

            // Create a TextFragmentAbsorber to search for the phrase "hello"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("hello")
            {
                TextSearchOptions = searchOptions
            };

            // Perform the search on the entire document
            absorber.Visit(doc);

            // Example modification: convert found text to uppercase
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = fragment.Text.ToUpperInvariant();
            }

            // Save the modified document
            doc.Save(outputPath);

            // Write the extraction error log (if any) to a text file
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                if (absorber.HasErrors)
                {
                    foreach (var error in absorber.Errors)
                    {
                        // The TextExtractionError class does not expose PageNumber or Message properties in
                        // some versions of Aspose.Pdf. Use its string representation which contains the relevant details.
                        writer.WriteLine(error.ToString());
                    }
                }
                else
                {
                    writer.WriteLine("No text extraction errors were logged.");
                }
            }

            Console.WriteLine($"Search completed. Log written to '{logPath}'.");
        }
    }
}
