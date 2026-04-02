using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfFootnoteExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF document with a single page
            using (Document doc = new Document())
            {
                // Add a blank page
                Page page = doc.Pages.Add();

                // Sample XML containing footnote definitions
                string footnoteXml = @"<Footnotes>
                                        <Footnote id='1'>This is the first footnote.</Footnote>
                                        <Footnote id='2'>Second footnote text goes here.</Footnote>
                                      </Footnotes>";

                // Load the XML into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(footnoteXml);

                // Select all footnote nodes
                XmlNodeList footnoteNodes = xmlDoc.SelectNodes("/Footnotes/Footnote");

                // Position variables for placing footnotes at the bottom of the page
                float startX = 50f;
                float startY = 50f; // distance from bottom
                float lineHeight = 12f;

                // Iterate over each footnote and add it to the page
                for (int i = 0; i < footnoteNodes.Count; i++)
                {
                    XmlNode footnoteNode = footnoteNodes[i];
                    string footnoteText = footnoteNode.InnerText.Trim();

                    // Create a Note object (footnote) and set its text
                    Aspose.Pdf.Note note = new Aspose.Pdf.Note();
                    note.Text = footnoteText; // <-- Fixed: use Text property instead of ActualText

                    // Create a TextFragment that will display the footnote reference number
                    TextFragment tf = new TextFragment((i + 1).ToString() + ". ");
                    tf.Position = new Position(startX, startY + i * lineHeight);
                    tf.TextState.FontSize = 8;
                    tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Attach the Note to the TextFragment
                    tf.FootNote = note;

                    // Append the TextFragment to the page
                    TextBuilder builder = new TextBuilder(page);
                    builder.AppendText(tf);
                }

                // Save the resulting PDF
                doc.Save("output.pdf");
            }
        }
    }
}
