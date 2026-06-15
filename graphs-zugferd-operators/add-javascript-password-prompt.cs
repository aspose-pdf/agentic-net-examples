using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Step 1: Create a simple sample PDF
        string inputPath = "input.pdf";
        using (Document doc = new Document())
        {
            // Add a single page
            doc.Pages.Add();
            // Add some visible text
            TextFragment fragment = new TextFragment("This is a sample PDF.");
            doc.Pages[1].Paragraphs.Add(fragment);
            doc.Save(inputPath);
        }

        // Step 2: Reopen the PDF and attach JavaScript that asks for a password
        using (Document doc = new Document(inputPath))
        {
            string javascript = "var pwd = app.response('Enter password to view this document:', 'Password');" +
                                "if (pwd != 'Secret123') { app.alert('Incorrect password'); this.closeDoc(); }";
            JavascriptAction jsAction = new JavascriptAction(javascript);
            doc.OpenAction = jsAction;
            doc.Save("output.pdf");
        }
    }
}