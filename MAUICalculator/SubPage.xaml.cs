namespace MAUICalculator;

public partial class SubPage : ContentPage
{
	public SubPage()
	{
		InitializeComponent();
	}
    int count = 0;
    const double cone = 2.7182;
    const double conpi = 3.1415;

    // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
    private double currentNumber = 0;
    private double lastNumber = 0;
    private string currentOperator = "";
    private bool isResult = false;
    public enum Choice { num, opt, equ, cnt, none };
    private Choice prechoice = Choice.none;

    // 定义OnNumberClicked方法来处理数字按钮点击事件
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var number = button.Text;

        prechoice = Choice.num;

        // 如果当前显示的是结果，或者是0，就清空显示屏
        if (isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            isResult = false;
        }

        // 将数字追加到显示屏，并更新当前输入的数字
        displayLabel.Text += number;
        currentNumber = double.Parse(displayLabel.Text);
    }

    //定义OnConstClicked方法来处理常量按钮点击事件
    private void OnConstClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        string number = "";

        prechoice = Choice.cnt;

        if (button.Text == "e")
        {
            number = cone.ToString();
        }
        else if (button.Text == "pi")
        {
            number = conpi.ToString();
        }

        displayLabel.Text = number;
        currentNumber = double.Parse(number);
    }

    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;

        // 如果当前的运算符不为空，就执行上一次选择的运算，并显示结果
        if (currentOperator != "")
        {
            Calculate();
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }
        else
        {
            // 否则，就将当前输入的数字赋值给上一次计算的结果
            if (op == "!")
            {
                bool sign = false;
                if (currentNumber < 0)
                {
                    currentNumber = -currentNumber;
                    sign = true;
                }
                int n = (int)currentNumber;
                long result = 1;
                for(int i = 1;i<=n;i++)
                {
                    result *= i;
                }
                currentNumber = sign ? -result : result;
                displayLabel.Text = currentNumber.ToString();
                prechoice = Choice.num;
                return;
            }
            else
            {
                lastNumber = currentNumber;
                displayLabel.Text = "0";
                isResult = false;
            }
            
        }

        // 将当前选择的运算符赋值给变量，并清空当前输入的数字
        currentOperator = op;
        prechoice = Choice.opt;
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnEqualClicked(object sender, EventArgs e)
    {
        prechoice = Choice.equ;

        // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
        if (currentOperator != "")
        {
            Calculate();
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
            currentOperator = "";
        }
    }

    // 定义OnClearClicked方法来处理清除按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        currentNumber = 0;
        lastNumber = 0;
        currentOperator = "";
        isResult = false;
        displayLabel.Text = lastNumber.ToString();
    }

    // 定义OnDeleteClicked方法来处理删除按钮点击事件
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        switch (prechoice)
        {
            case Choice.none:
                break;
            case Choice.num:
                {
                    var numstring = displayLabel.Text;
                    if (numstring.Length > 1)
                    {
                        numstring = numstring.Substring(0, numstring.Length - 1);
                    }
                    else
                    {
                        numstring = "0";
                    }
                    currentNumber = double.Parse(numstring);
                    displayLabel.Text = currentNumber.ToString();
                    break;
                }
            case Choice.opt:
                {
                    currentOperator = "";
                    break;
                }
            case Choice.equ:
                {
                    displayLabel.Text = "0";
                    break;
                }
            case Choice.cnt:
                {
                    if(currentOperator != "")
                    {
                        prechoice = Choice.opt;
                    }
                    else
                    {
                        prechoice = Choice.none;
                    }
                    displayLabel.Text = "0";
                    break;
                }
        }
    }

    // 定义Calculate方法来执行运算逻辑
    private void Calculate()
    {
        // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
        switch (currentOperator)
        {
            case "+":
                lastNumber += currentNumber;
                break;
            case "-":
                lastNumber -= currentNumber;
                break;
            case "*":
                lastNumber *= currentNumber;
                break;
            case "/":
                lastNumber /= currentNumber;
                break;
            case "power":
                lastNumber = Math.Pow(lastNumber, currentNumber);
                break;
            case "lg":
                lastNumber = Math.Log10(currentNumber);
                break;
            case "ln":
                lastNumber = Math.Log(currentNumber);
                break;
            case "√":
                lastNumber = Math.Sqrt(currentNumber);
                break;
            case "sin":
                lastNumber = Math.Sin(currentNumber * (Math.PI / 180));
                break;
            case "cos":
                lastNumber = Math.Cos(currentNumber * (Math.PI / 180));
                break;
            case "tan":
                lastNumber = Math.Tan(currentNumber * (Math.PI / 180));
                break;
            default:
                break;
        }
        lastNumber = Math.Round(lastNumber, 4);
        currentNumber = lastNumber;
    }
}