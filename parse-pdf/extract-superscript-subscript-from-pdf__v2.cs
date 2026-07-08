using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Use TextFragmentAbsorber to get access to individual TextFragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            // Accept the absorber for all pages
            doc.Pages.Accept(absorber);

            // Build the output with Unicode markers for superscript (^) and subscript (_)
            StringBuilder sb = new StringBuilder();

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                bool isSup = fragment.TextState.Superscript;
                bool isSub = fragment.TextState.Subscript;

                if (isSup)
                {
                    sb.Append('⁽');               // Unicode left superscript parenthesis as marker
                    sb.Append(fragment.Text);
                    sb.Append('⁾');               // Unicode right superscript parenthesis as marker
                }
                else if (isSub)
                {
                    sb.Append('₍');               // Unicode left subscript parenthesis as marker
                    sb.Append(fragment.Text);
                    sb.Append('₎');               // Unicode right subscript parenthesis as marker
                }
                else
                {
                    sb.Append(fragment.Text);
                }
            }

            // Save the extracted text with markers to a file
            File.WriteAllText(outputTxt, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Extraction complete. Output written to '{outputTxt}'.");
        }
    }
}
