import requests
from rich.console import Console
from rich.table import Table
from rich.panel import Panel
from rich.prompt import Prompt, IntPrompt

console = Console()
BASE_URL = "http://127.0.0.1:8000"
TOKEN = None


def show_banner():
    console.print(Panel.fit(
        "[bold cyan]MARTIAL ARTS STORE v1.0[/bold cyan]\n[italic]Пълна система за управление[/italic]",
        border_style="magenta"
    ))


def get_headers():
    return {"Authorization": f"Bearer {TOKEN}"}

def login():
    global TOKEN
    console.print("\n[bold yellow]Вход в системата[/bold yellow]")
    username = Prompt.ask("Потребител")
    password = Prompt.ask("Парола", password=True)

    try:
        response = requests.post(f"{BASE_URL}/login", data={"username": username, "password": password})
        if response.status_code == 200:
            TOKEN = response.json()["access_token"]
            console.print("[bold green]✔ Успешен вход![/bold green]")
        else:
            console.print("[bold red]✘ Грешно име или парола.[/bold red]")
    except:
        console.print("[bold red]✘ Сървърът не е пуснат! Проверете backend терминала.[/bold red]")


def list_products():
    if not TOKEN: return console.print("[red]Нужен е вход![/red]")

    search = Prompt.ask("Търсене на продукт по име (празно за всички)", default="")
    params = {"skip": 0, "limit": 10}
    if search: params["search"] = search

    resp = requests.get(f"{BASE_URL}/products", headers=get_headers(), params=params)
    if resp.status_code == 200:
        table = Table(title=f"Продукти (Филтър: {search if search else 'Няма'})")
        table.add_column("ID", style="dim")
        table.add_column("Име", style="cyan")
        table.add_column("Цена", style="green")
        table.add_column("Наличност", justify="center")

        for p in resp.json():
            table.add_row(str(p["id"]), p["name"], f"{p['price']:.2f} лв", str(p["stock_quantity"]))
        console.print(table)


def add_product():
    if not TOKEN: return console.print("[red]Нужен е вход![/red]")

    console.print("\n[bold blue]Добавяне на нов продукт[/bold blue]")
    name = Prompt.ask("Име на продукта")
    price = float(Prompt.ask("Цена"))
    stock = IntPrompt.ask("Количество на склад")
    cat_id = IntPrompt.ask("ID на категория (напр. 1)")

    payload = {"name": name, "price": price, "stock_quantity": stock, "category_id": cat_id}
    resp = requests.post(f"{BASE_URL}/products", json=payload, headers=get_headers())

    if resp.status_code == 200:
        console.print("[bold green]✔ Продуктът е добавен![/bold green]")
    else:
        console.print(f"[bold red]✘ Грешка: {resp.text}[/bold red]")


def add_customer():
    if not TOKEN: return console.print("[red]Нужен е вход![/red]")

    console.print("\n[bold blue]Регистрация на нов клиент[/bold blue]")
    first_name = Prompt.ask("Име")
    last_name = Prompt.ask("Фамилия")
    email = Prompt.ask("Имейл")
    phone = Prompt.ask("Телефон", default="")

    payload = {"first_name": first_name, "last_name": last_name, "email": email, "phone": phone, "is_active": True}
    resp = requests.post(f"{BASE_URL}/customers", json=payload, headers=get_headers())

    if resp.status_code == 200:
        console.print("[bold green]✔ Клиентът е добавен успешно![/bold green]")
    else:
        console.print(f"[bold red]✘ Грешка при запис.[/bold red]")


def search_customers():
    if not TOKEN: return console.print("[red]Нужен е вход![/red]")

    email_query = Prompt.ask("Търсене на клиент по имейл")
    params = {"email": email_query, "limit": 10}

    resp = requests.get(f"{BASE_URL}/customers", headers=get_headers(), params=params)
    if resp.status_code == 200:
        table = Table(title="Резултати за клиенти")
        table.add_column("ID", style="dim")
        table.add_column("Име", style="cyan")
        table.add_column("Имейл", style="magenta")

        for c in resp.json():
            table.add_row(str(c["id"]), f"{c['first_name']} {c['last_name']}", c["email"])
        console.print(table)


def main_menu():
    while True:
        show_banner()
        console.print("1. [bold white]Вход (Login)[/bold white]")
        console.print("-" * 30)
        console.print("2. [bold cyan]Продукти: Списък и Търсене[/bold cyan]")
        console.print("3. [bold cyan]Продукти: Добави нов[/bold cyan]")
        console.print("-" * 30)
        console.print("4. [bold blue]Клиенти: Търсене по имейл[/bold blue]")
        console.print("5. [bold blue]Клиенти: Добави нов[/bold blue]")
        console.print("-" * 30)
        console.print("6. [bold red]Изход[/bold red]")

        choice = Prompt.ask("\nИзберете опция", choices=["1", "2", "3", "4", "5", "6"])

        if choice == "1":
            login()
        elif choice == "2":
            list_products()
        elif choice == "3":
            add_product()
        elif choice == "4":
            search_customers()
        elif choice == "5":
            add_customer()
        elif choice == "6":
            break


if __name__ == "__main__":
    main_menu()