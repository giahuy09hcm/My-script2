using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DemoNUnit
{
    public class Class1
    {
        private IWebDriver driver;

        public object ExpectedConditions { get; private set; }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test()
        {
            //Vào màn hình Login
            driver.Url = "https://qc.vnresource.net/Login/Index";
            Thread.Sleep(1000);

            //Đăng nhập
            IWebElement txtUserNameById = driver.FindElement(By.Id("UserName"));
            txtUserNameById.SendKeys("huy.tran");
            Thread.Sleep(1000);
            IWebElement txtPasswordById = driver.FindElement(By.Id("Password"));
            txtPasswordById.SendKeys("123");
            Thread.Sleep(1000);
            IWebElement btnLogin = driver.FindElement(By.TagName("button"));
            btnLogin.Click();
            Thread.Sleep(1000);

            //Đi đến trang tạo công việc
            driver.Navigate().GoToUrl("https://qc.vnresource.net/Task_TaskList/Index");
            Thread.Sleep(1000);

            //Tạo công việc mới
            IWebElement btnAddNew = driver.FindElement(By.Id("btnAddNew"));
            btnAddNew.Click();
            Thread.Sleep(8000);

            //Kiểm tra trên lưới
            List<string> lstCellHeaderName = new List<string>();
            List<IWebElement> table1 = driver.FindElements(By.XPath("//div[@class='k-grid-header-locked']//th")).ToList();
            string header1 = "";
            List<IWebElement> table2 = driver.FindElements(By.XPath("//div[@class='k-grid-header-wrap k-auto-scrollable']//th")).ToList();
            string header2 = "";
            foreach (IWebElement a in table1)
            {
                header1 = a.Text;
                lstCellHeaderName.Add(header1);
            }
            foreach (IWebElement a in table2)
            {
                header2 = a.Text;
                lstCellHeaderName.Add(header2);
            }
            List<List<string>> lstCellData = new List<List<string>>();
            List<IWebElement> table3 = driver.FindElements(By.XPath("//div[@class='k-grid-content-locked']//tr")).ToList();
            List<IWebElement> table4 = driver.FindElements(By.XPath("//div[@class='k-grid-content k-auto-scrollable']//tr")).ToList();
            if(table3.Count()==0 || table4.Count() == 0)
            {
                IWebElement findTreebyID = driver.FindElement(By.XPath("//div[@id='treeview_Objectives']//child::span[text()='07. Lộ trình đào tạo']//ancestor::li"));
                IWebElement findChild = findTreebyID.FindElement(By.XPath(".//child::div"));
                IWebElement btnExpand = findTreebyID.FindElement(By.XPath(".//child::span[1]"));
                btnExpand.Click();
                Thread.Sleep(3000);
                IWebElement findChild1 = findTreebyID.FindElement(By.XPath(".//span[text()='07.02. Seminar, training']"));
                findChild1.Click();
                Thread.Sleep(1000);
                IWebElement txtJobTitleById = driver.FindElement(By.Id("FormCreate_ObjectiveItemName"));
                txtJobTitleById.SendKeys("Huy test auto");
                Thread.Sleep(1000);
                IWebElement txtDescriptionById = driver.FindElement(By.Id("FormCreate_Description"));
                txtDescriptionById.SendKeys("Huy làm bài tập auto");
                Thread.Sleep(1000);
                IWebElement btnCreateNewTaskById = driver.FindElement(By.Id("FormCreate_btnCreateNewTask"));
                btnCreateNewTaskById.Click();
                Thread.Sleep(3000);
                Console.WriteLine("Công việc 1" + ": vừa được tạo trên lưới");
            }
            else
            {
                foreach (IWebElement row1 in table3)
                {
                    List<string> rowData = new List<string>();
                    List<IWebElement> cells1 = row1.FindElements(By.XPath(".//td")).ToList();
                    foreach (IWebElement cell in cells1)
                    {
                        string data1 = cell.Text;
                        rowData.Add(data1);
                    }

                    int rowIndex = table3.IndexOf(row1);
                    if (rowIndex < table4.Count)
                    {
                        IWebElement row2 = table4[rowIndex];
                        List<IWebElement> cells2 = row2.FindElements(By.XPath(".//td")).ToList();
                        foreach (IWebElement cell in cells2)
                        {
                            string data2 = cell.Text;
                            rowData.Add(data2);
                        }
                    }

                    lstCellData.Add(rowData);
                }
                int checkNUM = 1;
                int checkJob = 0;
                foreach (List<string> rowData in lstCellData)
                {
                    string congViec = rowData[lstCellHeaderName.IndexOf("Công việc")];
                    string moTa = rowData[lstCellHeaderName.IndexOf("Mô tả")];

                    if (congViec == "Huy test auto" && moTa == "Huy làm bài tập auto")
                    {
                        IWebElement findCheckbox = table3[checkNUM-1].FindElement(By.XPath(".//td[2]"));
                        findCheckbox.Click();
                        IWebElement btnDeleteTasks = driver.FindElement(By.Id("FormCreate_btnDeleteTasks"));
                        btnDeleteTasks.Click();
                        Thread.Sleep(3000);
                        IWebElement btnOkay = driver.FindElement(By.XPath("//button[text()='Đồng ý']"));
                        btnOkay.Click();
                        Thread.Sleep(3000);
                        IWebElement findTreebyID = driver.FindElement(By.XPath("//div[@id='treeview_Objectives']//child::span[text()='07. Lộ trình đào tạo']//ancestor::li"));
                        IWebElement findChild = findTreebyID.FindElement(By.XPath(".//child::div"));
                        IWebElement btnExpand = findTreebyID.FindElement(By.XPath(".//child::span[1]"));
                        btnExpand.Click();
                        Thread.Sleep(3000);
                        IWebElement findChild1 = findTreebyID.FindElement(By.XPath(".//span[text()='07.02. Seminar, training']"));
                        findChild1.Click();
                        Thread.Sleep(1000);
                        IWebElement txtJobTitleById = driver.FindElement(By.Id("FormCreate_ObjectiveItemName"));
                        txtJobTitleById.SendKeys("Huy test auto");
                        Thread.Sleep(1000);
                        IWebElement txtDescriptionById = driver.FindElement(By.Id("FormCreate_Description"));
                        txtDescriptionById.SendKeys("Huy làm bài tập auto");
                        Thread.Sleep(1000);
                        IWebElement btnCreateNewTaskById = driver.FindElement(By.Id("FormCreate_btnCreateNewTask"));
                        btnCreateNewTaskById.Click();
                        Thread.Sleep(3000);
                        Console.WriteLine("Công việc " + checkNUM + ": vừa được xoá và thay thành công việc mới");
                        checkJob = 1;
                    }
                    checkNUM += 1;
                }
                if(checkJob!=1)
                {
                    IWebElement findTreebyID = driver.FindElement(By.XPath("//div[@id='treeview_Objectives']//child::span[text()='07. Lộ trình đào tạo']//ancestor::li"));
                    IWebElement findChild = findTreebyID.FindElement(By.XPath(".//child::div"));
                    IWebElement btnExpand = findTreebyID.FindElement(By.XPath(".//child::span[1]"));
                    btnExpand.Click();
                    Thread.Sleep(3000);
                    IWebElement findChild1 = findTreebyID.FindElement(By.XPath(".//span[text()='07.02. Seminar, training']"));
                    findChild1.Click();
                    Thread.Sleep(1000);
                    IWebElement txtJobTitleById = driver.FindElement(By.Id("FormCreate_ObjectiveItemName"));
                    txtJobTitleById.SendKeys("Huy test auto");
                    Thread.Sleep(1000);
                    IWebElement txtDescriptionById = driver.FindElement(By.Id("FormCreate_Description"));
                    txtDescriptionById.SendKeys("Huy làm bài tập auto");
                    Thread.Sleep(1000);
                    IWebElement btnCreateNewTaskById = driver.FindElement(By.Id("FormCreate_btnCreateNewTask"));
                    btnCreateNewTaskById.Click();
                    Thread.Sleep(3000);
                    Console.WriteLine("Công việc " + checkNUM + ": vừa được tạo trên lưới");
                }                    
            }           

        }

        [TearDown]
        public void Finish()
        {
            driver.Quit();
        }
    }
}
