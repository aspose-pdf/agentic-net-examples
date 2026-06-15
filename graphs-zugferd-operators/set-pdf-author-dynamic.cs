using System;
using Aspose.Pdf;

namespace SetPdfAuthor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Open the PDF and set author to current user
            using (Document doc = new Document("input.pdf"))
            {
                string currentUser = Environment.UserName;
                doc.Info.Author = currentUser;
                doc.Save("output.pdf");
            }

            Console.WriteLine("Author set to: " + Environment.UserName);
        }
    }
}
