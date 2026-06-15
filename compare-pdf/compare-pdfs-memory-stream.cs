using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Create first sample PDF and store it in a memory stream
        using (Document doc1 = new Document())
        {
            Page page1 = doc1.Pages.Add();
            page1.Paragraphs.Add(new TextFragment("Hello World"));
            using (MemoryStream ms1 = new MemoryStream())
            {
                doc1.Save(ms1);
                ms1.Position = 0;

                // Create second sample PDF with different content and store it in a memory stream
                using (Document doc2 = new Document())
                {
                    Page page2 = doc2.Pages.Add();
                    page2.Paragraphs.Add(new TextFragment("Hello Aspose"));
                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        doc2.Save(ms2);
                        ms2.Position = 0;

                        // Load PDFs from the memory streams
                        using (Document loadedDoc1 = new Document(ms1))
                        {
                            using (Document loadedDoc2 = new Document(ms2))
                            {
                                // Prepare a document that will hold the comparison result
                                using (Document resultDoc = new Document())
                                {
                                    GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                                    Page firstPageDoc1 = loadedDoc1.Pages[1];
                                    Page firstPageDoc2 = loadedDoc2.Pages[1];
                                    comparer.ComparePagesToPdf(firstPageDoc1, firstPageDoc2, resultDoc);

                                    // Save the diff PDF to another memory stream
                                    using (MemoryStream diffStream = new MemoryStream())
                                    {
                                        resultDoc.Save(diffStream);
                                        Console.WriteLine("Diff PDF size: " + diffStream.Length);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
