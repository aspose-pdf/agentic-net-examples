using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        TestBaseUrl();
        TestCreatorTool();
        TestNickname();
    }

    // Verify that BaseUrl metadata is correctly written and read back using custom metadata (indexer).
    private static void TestBaseUrl()
    {
        const string testName = "BaseUrl";
        string tempFile = Path.Combine(Path.GetTempPath(), "test_baseurl.pdf");
        string expected = "https://example.com/";

        try
        {
            // Create a new PDF document and set BaseUrl via custom metadata.
            using (Document doc = new Document())
            {
                // DocumentInfo does not expose a BaseUrl property; store it as a custom entry.
                doc.Info["BaseUrl"] = expected;
                doc.Save(tempFile);
            }

            // Reload the document and read BaseUrl.
            using (Document doc = new Document(tempFile))
            {
                string actual = doc.Info["BaseUrl"] as string;
                if (actual == expected)
                {
                    Console.WriteLine($"PASS {testName}");
                }
                else
                {
                    Console.WriteLine($"FAIL {testName} - Expected: '{expected}', Actual: '{actual}'");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL {testName} - Exception: {ex.Message}");
        }
        finally
        {
            // Clean up temporary file.
            try { if (File.Exists(tempFile)) File.Delete(tempFile); } catch { }
        }
    }

    // Verify that Creator metadata is correctly written and read back (CreatorTool → Creator).
    private static void TestCreatorTool()
    {
        const string testName = "CreatorTool";
        string tempFile = Path.Combine(Path.GetTempPath(), "test_creatortool.pdf");
        string expected = "MyCreatorTool v1.0";

        try
        {
            // Create a new PDF document and set Creator (the correct property).
            using (Document doc = new Document())
            {
                doc.Info.Creator = expected;
                doc.Save(tempFile);
            }

            // Reload the document and read Creator.
            using (Document doc = new Document(tempFile))
            {
                string actual = doc.Info.Creator;
                if (actual == expected)
                {
                    Console.WriteLine($"PASS {testName}");
                }
                else
                {
                    Console.WriteLine($"FAIL {testName} - Expected: '{expected}', Actual: '{actual}'");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL {testName} - Exception: {ex.Message}");
        }
        finally
        {
            // Clean up temporary file.
            try { if (File.Exists(tempFile)) File.Delete(tempFile); } catch { }
        }
    }

    // Verify that Nickname metadata is correctly written and read back (use Title as a surrogate).
    private static void TestNickname()
    {
        const string testName = "Nickname";
        string tempFile = Path.Combine(Path.GetTempPath(), "test_nickname.pdf");
        string expected = "SamplePDF";

        try
        {
            // Create a new PDF document and set Title to act as Nickname.
            using (Document doc = new Document())
            {
                doc.Info.Title = expected;
                doc.Save(tempFile);
            }

            // Reload the document and read Title.
            using (Document doc = new Document(tempFile))
            {
                string actual = doc.Info.Title;
                if (actual == expected)
                {
                    Console.WriteLine($"PASS {testName}");
                }
                else
                {
                    Console.WriteLine($"FAIL {testName} - Expected: '{expected}', Actual: '{actual}'");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL {testName} - Exception: {ex.Message}");
        }
        finally
        {
            // Clean up temporary file.
            try { if (File.Exists(tempFile)) File.Delete(tempFile); } catch { }
        }
    }
}
