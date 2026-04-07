using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            string js = "Date expiry = new Date('2025-12-31'); Date now = new Date(); if (now > expiry) { app.alert('This document has expired.'); this.closeDoc(); }";

            doc.OpenAction = new JavascriptAction(js);

            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with expiry JavaScript saved as output.pdf");
    }
}