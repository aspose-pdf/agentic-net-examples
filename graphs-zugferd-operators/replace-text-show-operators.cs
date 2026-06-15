using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Operators;

namespace ReplaceTextByOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with known text
            using (Document sampleDoc = new Document())
            {
                // Add a page (evaluation mode allows up to 4 pages)
                Page page = sampleDoc.Pages.Add();

                // Add a text fragment "Hello World"
                TextFragment fragment = new TextFragment("Hello World");
                page.Paragraphs.Add(fragment);

                // Save the sample PDF
                sampleDoc.Save("sample.pdf");
            }

            // Step 2: Open the PDF and replace the text by editing operators
            using (Document doc = new Document("sample.pdf"))
            {
                // Iterate through pages (limit to first 4 pages)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count && pageIndex <= 4; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    OperatorCollection operators = page.Contents;

                    // Iterate through operators
                    for (int opIndex = 1; opIndex <= operators.Count; opIndex++)
                    {
                        Aspose.Pdf.Operator op = operators[opIndex];
                        if (op is ShowText)
                        {
                            ShowText showText = (ShowText)op;
                            if (showText.Text == "Hello World")
                            {
                                // Replace the text
                                showText.Text = "Hi Aspose";
                            }
                        }
                    }
                }

                // Save the modified PDF
                doc.Save("output.pdf");
            }
        }
    }
}
