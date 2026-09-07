from time import perf_counter


class Biblioteca:
    def __init__(self):
        # Diccionario/mapa principal:
        # ISBN -> información del libro
        self.libros = {}

        # Conjunto de categorías sin duplicados
        self.categorias = set()

    def registrar_libro(self, isbn, titulo, autor, categoria):
        """Registra un libro en la biblioteca."""
        if isbn in self.libros:
            return False, "El ISBN ya está registrado."

        self.libros[isbn] = {
            "titulo": titulo,
            "autor": autor,
            "categoria": categoria,
            "disponible": True,
        }

        self.categorias.add(categoria)

        return True, "Libro registrado correctamente."

    def consultar_libro(self, isbn):
        """Busca un libro mediante su ISBN."""
        return self.libros.get(isbn)

    def eliminar_libro(self, isbn):
        """Elimina un libro."""
        if isbn not in self.libros:
            return False, "El libro no existe."

        del self.libros[isbn]
        return True, "Libro eliminado correctamente."

    def prestar_libro(self, isbn):
        """Registra el préstamo de un libro."""
        libro = self.libros.get(isbn)

        if libro is None:
            return False, "El libro no existe."

        if not libro["disponible"]:
            return False, "El libro ya está prestado."

        libro["disponible"] = False
        return True, "Préstamo registrado correctamente."

    def devolver_libro(self, isbn):
        """Registra la devolución de un libro."""
        libro = self.libros.get(isbn)

        if libro is None:
            return False, "El libro no existe."

        if libro["disponible"]:
            return False, "El libro ya estaba disponible."

        libro["disponible"] = True
        return True, "Devolución registrada correctamente."

    def mostrar_libros(self):
        """Muestra todos los libros."""
        if not self.libros:
            print("\nNo existen libros registrados.")
            return

        print("\n========== LIBROS REGISTRADOS ==========")

        for isbn, libro in self.libros.items():
            estado = "Disponible" if libro["disponible"] else "Prestado"

            print(f"ISBN: {isbn}")
            print(f"Título: {libro['titulo']}")
            print(f"Autor: {libro['autor']}")
            print(f"Categoría: {libro['categoria']}")
            print(f"Estado: {estado}")
            print("-" * 40)

    def libros_por_categoria(self, categoria):
        """Obtiene los libros de una determinada categoría."""
        resultado = []

        for isbn, libro in self.libros.items():
            if libro["categoria"].lower() == categoria.lower():
                resultado.append((isbn, libro))

        return resultado

    def libros_disponibles(self):
        """Obtiene únicamente los libros disponibles."""
        return [
            (isbn, libro)
            for isbn, libro in self.libros.items()
            if libro["disponible"]
        ]

    def reporte(self):
        """Genera un reporte estadístico."""
        total = len(self.libros)

        disponibles = sum(
            1 for libro in self.libros.values() if libro["disponible"]
        )

        prestados = total - disponibles

        print("\n========== REPORTE DE BIBLIOTECA ==========")
        print(f"Total de libros: {total}")
        print(f"Libros disponibles: {disponibles}")
        print(f"Libros prestados: {prestados}")
        print(f"Cantidad de categorías: {len(self.categorias)}")

        print("\nCategorías registradas:")

        for categoria in sorted(self.categorias):
            print(f"- {categoria}")

    def medir_busqueda(self, isbn, repeticiones=10000):
        """Mide el tiempo de búsqueda de un libro."""
        inicio = perf_counter()

        for _ in range(repeticiones):
            self.consultar_libro(isbn)

        fin = perf_counter()

        tiempo_total = fin - inicio
        tiempo_promedio = tiempo_total / repeticiones

        return tiempo_total, tiempo_promedio


def cargar_datos_demo(biblioteca):
    """Carga datos de prueba."""
    datos = [
        ("9780135957059", "Python Programming", "Autor Uno", "Programación"),
        ("9781492052203", "Fluent Python", "Autor Dos", "Programación"),
        ("9780134494166", "Java Programming", "Autor Tres", "Programación"),
        (
            "9780262046305",
            "Inteligencia Artificial",
            "Autor Cuatro",
            "Inteligencia Artificial",
        ),
        (
            "9780132350884",
            "Clean Code",
            "Robert Martin",
            "Ingeniería de Software",
        ),
    ]

    for isbn, titulo, autor, categoria in datos:
        biblioteca.registrar_libro(isbn, titulo, autor, categoria)


def menu():
    biblioteca = Biblioteca()
    cargar_datos_demo(biblioteca)

    while True:
        print("\n======================================")
        print("       SISTEMA DE BIBLIOTECA")
        print("======================================")
        print("1. Registrar libro")
        print("2. Consultar libro por ISBN")
        print("3. Mostrar todos los libros")
        print("4. Buscar por categoría")
        print("5. Prestar libro")
        print("6. Devolver libro")
        print("7. Mostrar libros disponibles")
        print("8. Mostrar categorías")
        print("9. Mostrar reporte")
        print("10. Medir tiempo de búsqueda")
        print("11. Eliminar libro")
        print("0. Salir")

        opcion = input("\nSeleccione una opción: ")

        if opcion == "1":
            isbn = input("ISBN: ")
            titulo = input("Título: ")
            autor = input("Autor: ")
            categoria = input("Categoría: ")

            ok, mensaje = biblioteca.registrar_libro(isbn, titulo, autor, categoria)
            print(mensaje)

        elif opcion == "2":
            isbn = input("Ingrese el ISBN: ")

            libro = biblioteca.consultar_libro(isbn)

            if libro:
                print("\nLibro encontrado:")
                print(f"Título: {libro['titulo']}")
                print(f"Autor: {libro['autor']}")
                print(f"Categoría: {libro['categoria']}")
                print("Estado:", "Disponible" if libro["disponible"] else "Prestado")
            else:
                print("No se encontró el libro.")

        elif opcion == "3":
            biblioteca.mostrar_libros()

        elif opcion == "4":
            categoria = input("Ingrese la categoría: ")

            resultados = biblioteca.libros_por_categoria(categoria)

            if not resultados:
                print("No existen libros en esa categoría.")
            else:
                print(f"\nLibros de la categoría '{categoria}':")

                for isbn, libro in resultados:
                    print(f"{isbn} - {libro['titulo']} - {libro['autor']}")

        elif opcion == "5":
            isbn = input("ISBN del libro a prestar: ")

            ok, mensaje = biblioteca.prestar_libro(isbn)
            print(mensaje)

        elif opcion == "6":
            isbn = input("ISBN del libro a devolver: ")

            ok, mensaje = biblioteca.devolver_libro(isbn)
            print(mensaje)

        elif opcion == "7":
            disponibles = biblioteca.libros_disponibles()

            print("\n========== LIBROS DISPONIBLES ==========")

            for isbn, libro in disponibles:
                print(f"{isbn} - {libro['titulo']} - {libro['autor']}")

        elif opcion == "8":
            print("\n========== CATEGORÍAS ==========")

            for categoria in sorted(biblioteca.categorias):
                print("-", categoria)

        elif opcion == "9":
            biblioteca.reporte()

        elif opcion == "10":
            isbn = input("ISBN a consultar: ")

            if isbn in biblioteca.libros:
                total, promedio = biblioteca.medir_busqueda(isbn)

                print("\n========== ANÁLISIS DE TIEMPO ==========")
                print("Repeticiones: 10000")
                print(f"Tiempo total: {total:.8f} segundos")
                print(f"Tiempo promedio: {promedio:.10f} segundos")
            else:
                print("El ISBN no existe.")

        elif opcion == "11":
            isbn = input("ISBN del libro a eliminar: ")

            ok, mensaje = biblioteca.eliminar_libro(isbn)
            print(mensaje)

        elif opcion == "0":
            print("Programa finalizado.")
            break

        else:
            print("Opción inválida.")


if __name__ == "__main__":
    menu()
