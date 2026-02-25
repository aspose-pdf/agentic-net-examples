using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new blank PDF document inside a using block
        // to ensure deterministic disposal of file handles.
        using (Document doc = new Document())
        {
            // Save the blank document as PDF.
            // Document.Save(string) without SaveOptions always writes PDF.
            doc.Save("blank.pdf");
        }

        Console.WriteLine("Blank PDF created: blank.pdf");
    }
}