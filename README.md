# Unity Tilemap Material Setter

Unity Tilemap Material Setter - это инструмент для редактора Unity, который автоматизирует процесс установки материалов для всех компонентов `TilemapRenderer` во всех сценах, находящихся в указанной директории.

## Зачем это нужно

В крупных проектах Unity с множеством сцен и тайлмапов вручную изменять материал для каждого `TilemapRenderer` может быть трудоемким и подверженным ошибкам процессом. Этот инструмент решает данную проблему, автоматизируя этот процесс и экономя ваше время.

<img width="504" alt="image" src="https://github.com/RimuruDev/Unity-TilemapMaterialSetter/assets/85500556/1325ffbb-56c7-47b4-af62-632aff6bb927">
<img width="504" alt="image" src="https://github.com/RimuruDev/Unity-TilemapMaterialSetter/assets/85500556/fab8adcd-d541-4282-a321-ab338879f08d">


## Как использовать

### Установка

1. Скачайте и распакуйте [последний релиз](https://github.com/RimuruDev/Unity-TilemapMaterialSetter/releases) или склонируйте репозиторий:

    ```bash
    git clone https://github.com/RimuruDev/Unity-TilemapMaterialSetter.git
    ```

2. Поместите папку `Editor` в директорию вашего проекта Unity.

### Использование

1. Откройте Unity и дождитесь компиляции скрипта.
2. В меню Unity выберите `RimuruDev Tools > Set Tilemap Material`.
3. В открывшемся окне введите путь к материалу и путь к папке со сценами.
    - `Material Path`: Путь к материалу, который вы хотите установить (например, `Assets/Internal/Common/PixelSnapMat.mat`).
    - `Scenes Path`: Путь к директории, содержащей ваши сцены (например, `Assets/Internal/Scenes/Levels`).
4. Нажмите кнопку `Set Material for All Tilemaps`.

Скрипт откроет каждую сцену из указанной директории, найдет все компоненты `TilemapRenderer` и установит для них указанный материал. После этого он сохранит изменения в сценах.

# Автор
Этот инструмент разработан RimuruDev. Вы можете найти больше проектов на моем GitHub.

# Лицензия
Этот проект лицензирован под лицензией MIT. Подробнее см. в файле LICENSE.
