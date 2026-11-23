using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a tagged content tree
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Add a new page that will hold the Table of Contents
            Page tocPage = doc.Pages.Add();

            // Visible TOC title
            TextFragment titleTf = new TextFragment("Table of Contents");
            titleTf.TextState.FontSize = 20;
            titleTf.TextState.FontStyle = FontStyles.Bold;
            tocPage.Paragraphs.Add(titleTf);

            // Visible TOC entries (simple list)
            string[] entries = new[]
            {
                "1. Introduction...........................................1",
                "2. Chapter One...........................................5",
                "   2.1 Section A...........................................6",
                "   2.2 Section B...........................................8",
                "3. Conclusion...........................................12"
            };

            foreach (string entry in entries)
            {
                TextFragment tf = new TextFragment(entry);
                tf.TextState.FontSize = 12;
                tocPage.Paragraphs.Add(tf);
            }

            // Build the logical structure for the TOC
            var root = taggedContent.RootElement; // root of the tagged structure tree

            // Container for the TOC (Div)
            DivElement tocDiv = taggedContent.CreateDivElement();
            // Use the Title property (DivElement does not have SetText)
            tocDiv.Title = "Table of Contents";
            root.AppendChild(tocDiv);

            // Title element (Paragraph)
            ParagraphElement titlePara = taggedContent.CreateParagraphElement();
            titlePara.SetText("Table of Contents");
            tocDiv.AppendChild(titlePara);

            // First‑level and sub‑level entries (Paragraphs)
            foreach (string entry in entries)
            {
                bool isSubEntry = entry.StartsWith("   ");
                ParagraphElement para = taggedContent.CreateParagraphElement();
                para.SetText(entry.Trim());

                if (isSubEntry)
                {
                    // Find or create a sub‑list container (Div) inside the main TOC Div
                    DivElement subDiv = null;
                    foreach (Element child in tocDiv.ChildElements)
                    {
                        if (child is DivElement d && d.AlternativeText == "SubList")
                        {
                            subDiv = d;
                            break;
                        }
                    }
                    if (subDiv == null)
                    {
                        subDiv = taggedContent.CreateDivElement();
                        subDiv.AlternativeText = "SubList";
                        tocDiv.AppendChild(subDiv);
                    }
                    subDiv.AppendChild(para);
                }
                else
                {
                    tocDiv.AppendChild(para);
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with accessible Table of Contents saved to '{outputPath}'.");
    }
}
