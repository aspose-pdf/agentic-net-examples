using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        // Prepare a simple XML file that defines PDF content
        string xmlPath = "sample.xml";
        string xmlContent = "<?xml version='1.0' encoding='utf-8'?>\n<pdf>\n  <pages>\n    <page>\n      <paragraphs>\n        <paragraph>\n          <text>Hello, Aspose PDF!</text>\n        </paragraph>\n      </paragraphs>\n    </page>\n  </pages>\n</pdf>";
        File.WriteAllText(xmlPath, xmlContent);

        // Create a PDF document from the XML definition
        using (Document document = new Document())
        {
            // Use the Stream overload of BindXml to avoid Uri parsing issues
            using (MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
            {
                document.BindXml(xmlStream);
            }

            // Apply a fade transition to every page (duration 1 second)
            // Do NOT wrap PdfPageEditor in a using block because its Dispose() also disposes the bound Document.
            PdfPageEditor editor = new PdfPageEditor(document);
            int[] allPages = new int[document.Pages.Count];
            for (int i = 0; i < allPages.Length; i++)
                allPages[i] = i + 1; // pages are 1‑based

            editor.ProcessPages = allPages;
            editor.TransitionType = 11;          // 11 = Fade transition
            editor.TransitionDuration = 1;       // 1 second
            editor.ApplyChanges();

            // Save the resulting PDF
            document.Save("output.pdf");

            // Now it is safe to dispose the editor
            editor.Dispose();
        }
    }
}
