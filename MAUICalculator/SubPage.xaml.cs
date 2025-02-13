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

    // ����һЩ�������洢��ǰ��������֣���ǰѡ�����������Լ���һ�μ���Ľ��
    private double currentNumber = 0;
    private double lastNumber = 0;
    private string currentOperator = "";
    private bool isResult = false;
    public enum Choice { num, opt, equ, cnt, none };
    private Choice prechoice = Choice.none;

    // ����OnNumberClicked�������������ְ�ť����¼�
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var number = button.Text;

        prechoice = Choice.num;

        // �����ǰ��ʾ���ǽ����������0���������ʾ��
        if (isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            isResult = false;
        }

        // ������׷�ӵ���ʾ���������µ�ǰ���������
        displayLabel.Text += number;
        currentNumber = double.Parse(displayLabel.Text);
    }

    //����OnConstClicked��������������ť����¼�
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
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var op = button.Text;

        // �����ǰ���������Ϊ�գ���ִ����һ��ѡ������㣬����ʾ���
        if (currentOperator != "")
        {
            Calculate();
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }
        else
        {
            // ���򣬾ͽ���ǰ��������ָ�ֵ����һ�μ���Ľ��
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

        // ����ǰѡ����������ֵ������������յ�ǰ���������
        currentOperator = op;
        prechoice = Choice.opt;
    }

    // ����OnEqualClicked����������ȺŰ�ť����¼�
    private void OnEqualClicked(object sender, EventArgs e)
    {
        prechoice = Choice.equ;

        // �����ǰѡ����������Ϊ�գ���ִ����һ��ѡ������㣬����ʾ���
        if (currentOperator != "")
        {
            Calculate();
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
            currentOperator = "";
        }
    }

    // ����OnClearClicked���������������ť����¼�
    private void OnClearClicked(object sender, EventArgs e)
    {
        currentNumber = 0;
        lastNumber = 0;
        currentOperator = "";
        isResult = false;
        displayLabel.Text = lastNumber.ToString();
    }

    // ����OnDeleteClicked����������ɾ����ť����¼�
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

    // ����Calculate������ִ�������߼�
    private void Calculate()
    {
        // ���ݵ�ǰѡ��������������һ�μ���Ľ���͵�ǰ��������ֽ�����Ӧ�����㣬��������һ�μ���Ľ��
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
            case "��":
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