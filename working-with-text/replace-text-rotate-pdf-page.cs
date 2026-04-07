using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF in memory that contains the text "Hello World"
        using (MemoryStream inputStream = new MemoryStream())
        {
            using (Document tempDoc = new Document())
            {
                Page page = tempDoc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Hello World"));
                tempDoc.Save(inputStream);
            }

            // Reset the stream position before reading it back
            inputStream.Position = 0;

            // Load the PDF from the memory stream
            using (Document doc = new Document(inputStream))
            {
                // ---------- Text replacement ----------
                TextFragmentAbsorber absorber = new TextFragmentAbsorber("Hello World");
                doc.Pages.Accept(absorber);
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = "Hi Universe";
                }

                // ---------- Page rotation ----------
                if (doc.Pages.Count >= 1)
                {
                    Page firstPage = doc.Pages[1]; // 1‑based indexing
                    firstPage.Rotate = Rotation.on90; // 90° clockwise
                }

                // Save the modified document into a memory stream (PDF format)
                using (MemoryStream outputStream = new MemoryStream())
                {
                    doc.Save(outputStream);
                    // Optional: write the stream to a physical file for verification
                    File.WriteAllBytes("modified_output.pdf", outputStream.ToArray());
                }
            }
        }
    }
}
