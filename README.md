# Изработено од
Андреј Арсовски-231170, Мартина Ивановска-231014 и Мила Спасевска-231046

# FitAppMVVM
# **FitJournal - Апликација за Следење на Тренинзи**

## **1\. Опис на апликацијата**

FitJournal е крос-платформска апликација за следење на тренинг сесии, развиена со Uno Platform. Апликацијата им овозможува на корисниците да креираат и менаџираат тренинзи, да додаваат вежби и да го следат својот напредок преку календарски приказ.

Апликацијата работи на Windows, Android, iOS и macOS, и поддржува локално чување на податоци со SQLite без потреба од интернет.

### **Клучни функционалности:**

- Додавање и бришење тренинзи и вежби  

- Внесување податоци како име на тренинг, датуми, серии, повторувања, тежина, белешки  

- Приказ на тренинзите во календар според датум  

- Локално чување на податоци со SQLite база  

- Интуитивен и минималистички интерфејс  

## **2\. Упатство за користење**

### **2.1 Додавање нов тренинг**

На главниот екран корисникот може да внесе име и белешки за нов тренинг, кој автоматски добива тековен датум. По внесувањето, тренингот се чува и се прикажува во листата на тренинзи.

### **2.2 Додавање вежби во тренинг**

Откако е создаден тренинг, корисникот може да влезе во деталниот приказ на тренингот и да додава вежби. За секоја вежба се внесуваат:

- Име на вежба
- Број на серии
- Број на повторувања
- Тежина

### **2.3 Календарски приказ**

Преку CalendarPage корисникот може да ги прегледа тренинзите групирани според датумот.

### **2.4 Бришење**

Корисникот може да брише тренинзи или поединечни вежби.

## **3\. Претставување на проблемот**

### **3.1 Податочни структури**

Главните модели што ги користи апликацијата се:

```
public class Workout
{

\[PrimaryKey, AutoIncrement\]

public int Id { get; set; }

public string Name { get; set; }

public DateTime Date { get; set; }

public string Notes { get; set; }

}
```

```
public class WorkoutExercise

{

\[PrimaryKey, AutoIncrement\]

public int Id { get; set; }

public int WorkoutId { get; set; } // Врска кон тренинг

public string ExerciseName { get; set; }

public int Sets { get; set; }

public int Reps { get; set; }

public double Weight { get; set; }

}
```

###

### **3.2 Логика**

#### 3.2.1 HomePageViewModel

ViewModel-от на HomePage се грижи за прикажување на најновите 7 тренинзи, кои се преземаат од SQLite базата:

var latestSeven = workoutsFromDb.OrderByDescending(w => w.Date).Take(7).ToList();  

#### 3.2.2 HomePage.xaml.cs

Во code-behind логиката на HomePage, овозможени се следниве акции:

- GoToWorkoutsPage_Click() – Префрлување на страната со сите тренинзи
- GoToCalendarPage_Click() – Преглед на тренинзите преку календар
- WorkoutListView_ItemClick() – Отворање на деталите за избран тренинг
- DeleteButton_Click() – Бришење на тренинг од базата и веднаш од листата
```
private void WorkoutListView_ItemClick(object sender, ItemClickEventArgs e)

{

if (e.ClickedItem is Workout selectedWorkout)

Frame.Navigate(typeof(WorkoutDetailsPage), selectedWorkout);

}
```
#### 3.2.3 AddExercisePage

ViewModel класата AddExerciseViewModel овозможува внесување на нова вежба поврзана со даден тренинг (WorkoutId). Податоците се добиваат преку binding од XAML формата. Валидацијата спречува внес без име, а по успешното зачувување, полињата автоматски се ресетираат
```
public async Task AddExercise()

{

if (string.IsNullOrWhiteSpace(ExerciseName))

return;

await DatabaseService.InitAsync();

var exercise = new WorkoutExercise

{

ExerciseName = ExerciseName,

Sets = ExerciseSets,

Reps = ExerciseReps,

Weight = ExerciseWeight,

WorkoutId = WorkoutId

};

await DatabaseService.AddExerciseAsync(exercise);

// Чистење на полињата

ExerciseName = string.Empty;

ExerciseSets = 0;

ExerciseReps = 0;

ExerciseWeight = 0;

}  
```
#### 3.2.4 AddExercisePage.xaml.cs

Класата AddExercisePage е страната за додавање на вежба. При навигација кон неа, добива параметар (WorkoutId) од претходната страница и го инстанцира ViewModel-от.
```
protected override void OnNavigatedTo(NavigationEventArgs e)

{

base.OnNavigatedTo(e);

if (e.Parameter is int workoutId)

{

\_viewModel = new AddExerciseViewModel(workoutId);

this.DataContext = \_viewModel;

}

}
```
#### 3.2.5 WorkoutDetailsPage.xaml.cs

Страницата WorkoutDetailsPage се користи за прикажување на детали за одреден тренинг (име, белешки) и листа на вежби поврзани со тој тренинг. Исто така, корисникот може да додаде нова вежба или да избрише постоечка.

Код: Навигација со параметар
```
protected override void OnNavigatedTo(NavigationEventArgs e)

{

base.OnNavigatedTo(e);

if (e.Parameter is Workout workout)

{

\_workoutId = workout.Id; // Store the ID

\_viewModel = new WorkoutDetailsViewModel(workout.Id);

TextBlockName.Text = workout.Name;

TextBlockNote.Text = workout.Notes;

this.DataContext = \_viewModel;

}

}
```
#### **Код: Бришење на вежба**
```
private async void DeleteExercise_Click(object sender, RoutedEventArgs e)

{

if (sender is Button button && button.DataContext is WorkoutExercise exercise)

{

await DatabaseService.InitAsync();

await DatabaseService.DeleteExerciseAsync(exercise.Id);

\_viewModel.Exercises.Remove(exercise);

}

}
```
#### 3.2.6 WorkoutDetailsViewModel.cs

ViewModel-от WorkoutDetailsViewModel ја вчитува листата на вежби за даден тренинг при иницијализација. Се користи ObservableCollection&lt;WorkoutExercise&gt; за автоматско освежување на интерфејсот при промени (на пр. бришење).
```
public WorkoutDetailsViewModel(int workoutId)

{

\_workoutId = workoutId;

LoadExercises();

}

private async void LoadExercises()

{

await DatabaseService.InitAsync();

var exercises = await DatabaseService.GetExercisesByWorkoutIdAsync(\_workoutId);

Exercises.Clear();

foreach (var exercise in exercises)

{

Exercises.Add(exercise);

}

}
```
####

### **3.3 Локална база на податоци**

Сите податоци се чуваат локално во SQLite база преку класата DatabaseService, која овозможува додавање, бришење и читање на тренинзи и вежби.

## **4\. Изглед на апликацијата**

- **HomePage:** Листа на тренинзи, копче за додавање нов тренинг и копче за преглед на календарот
![Screenshot HomePage](https://github.com/user-attachments/assets/a28514b3-63c7-447a-96cc-94c0c94500f8)


- **AddWorkoutPage:** Форма за внес на нова вежба

![Screenshot AddWorkout](https://github.com/user-attachments/assets/54aa7bf8-539b-4641-9f44-fc95d5d73e16)


- **WorkoutDetailsPage:** Приказ на вежби за одреден тренинг

![Screenshot WorkoutDetails ](https://github.com/user-attachments/assets/1d8bedb6-4ba9-4897-a5db-9fa15862da26)


- **AddExercisePage:** Форма за внес на нова вежба

![Screenshot AddExercise](https://github.com/user-attachments/assets/a3beb0b0-eef1-44ca-a6fb-477b3532b5f9)


- **CalendarPage:** Календарски приказ на тренинзи

![Screenshot Calendar](https://github.com/user-attachments/assets/ea19d955-be25-4499-ac3d-e4f1791d6bac)


- **DayDetailsPage:** Сортирање на тренинзите според датум

![Screenshot DayDetails](https://github.com/user-attachments/assets/168bae96-b0bc-4471-854c-6aef0a29bc3c)


- **Light/Dark mode:** Апликацијата е во Light или Dark mode во зависност од кој е default app mode локално на уредот на кој се извршува.
![Screenshot Light/Dark mode](https://github.com/user-attachments/assets/fbe0760c-f40b-4640-b4a3-749849167043)



## **5\. Технологии и алатки**

- **Uno Platform** – Крос-платформски UI framework  

- **C# / MVVM** – Логика и архитектура на апликацијата  

- **SQLite** – Локална база за чување податоци  

- **CommunityToolkit.Mvvm** – Помош за MVVM pattern

## **6\. Генерална употреба на вештачка интелегенција**

### 6.1 Модел

- ChatGpt-4o
- DeepSeek-V3

### 6.2 Подобрување на UI

- Prompt:

Интерфејс дизајниран во Canva

![Screenshot Canva](https://github.com/user-attachments/assets/e0d5a5e3-3381-4c95-9485-e63fb9d6e533)

```
&lt;Page x:Class="FitAppMVVM.Presentation.HomePage" x:Name="RootPage" xmlns="<http://schemas.microsoft.com/winfx/2006/xaml/presentation>" xmlns:x="<http://schemas.microsoft.com/winfx/2006/xaml>" xmlns:local="using:FitAppMVVM.Presentation" xmlns:d="<http://schemas.microsoft.com/expression/blend/2008>" xmlns:mc="<http://schemas.openxmlformats.org/markup-compatibility/2006>" mc:Ignorable="d" Background="{ThemeResource ApplicationPageBackgroundThemeBrush}"&gt; &lt;Grid Padding="24"&gt; &lt;Grid.RowDefinitions&gt; &lt;RowDefinition Height="\*"/&gt; &lt;RowDefinition Height="Auto"/&gt; &lt;/Grid.RowDefinitions&gt; &lt;!-- Workout List --&gt; &lt;ListView ItemsSource="{Binding Workouts}" IsItemClickEnabled="True" ItemClick="WorkoutListView_ItemClick" Margin="0,0,0,16" &gt; &lt;ListView.ItemTemplate&gt; &lt;DataTemplate&gt; &lt;Border Margin="0,0,0,12" Padding="16" Background="{ThemeResource SystemControlBackgroundAccentBrush}" CornerRadius="12"&gt; &lt;StackPanel&gt; &lt;TextBlock Text="{Binding Name}" FontSize="18" FontWeight="Bold" Foreground="White"/&gt; &lt;TextBlock Text="{Binding Notes}" FontSize="14" Foreground="White" Margin="0,4,0,8"/&gt; &lt;Button Content="Delete" HorizontalAlignment="Right" Click="DeleteButton_Click" /&gt; &lt;/StackPanel&gt; &lt;/Border&gt; &lt;/DataTemplate&gt; &lt;/ListView.ItemTemplate&gt; &lt;/ListView&gt; &lt;!-- Bottom Buttons --&gt; &lt;StackPanel Grid.Row="1" HorizontalAlignment="Center" VerticalAlignment="Bottom" Spacing="16" Orientation="Horizontal"&gt; &lt;Button Content="Add New Workout" Click="GoToWorkoutsPage_Click" /&gt; &lt;Button Content="Calendar View" Click="GoToCalendarPage_Click" /&gt; &lt;/StackPanel&gt; &lt;/Grid&gt; &lt;/Page&gt;
```
Here is the code i currently have and here is the picture of how i would like it to look. Make the changes in the code.

### 6.3 Debugging

- Prompt:
```
namespace FitAppMVVM.Presentation
 {
public partial class AddExerciseViewModel:ObservableObject {
[ObservableProperty] private string exerciseName;
[ObservableProperty] private int exerciseSets;
[ObservableProperty] private int exerciseReps;
[ObservableProperty] private int exerciseWeight;
 public int workoutId;
//public ObservableCollection&lt;WorkoutExercise&gt;
 WorkoutExercises { get; } = new(); \[RelayCommand\]
public async Task AddExercise() {
await DatabaseService.InitAsync();
 var exercise = new WorkoutExercise { ExerciseName = ExerciseName, Sets = ExerciseSets, Reps = ExerciseReps, Weight = ExerciseWeight, WorkoutId = this.workoutId };
Console.WriteLine($"Adding exercise with WorkoutId={exercise.WorkoutId}");
 Console.WriteLine($"Adding exercise with WorkoutId={exercise.ExerciseName}");
 await DatabaseService.AddExerciseAsync(exercise);
 //ExerciseName = string.Empty;
ExerciseSets = 0;
ExerciseWeight =0;
ExerciseReps = 0; } } }
```
it looks like the binding doesn't work properly and in the database the exercise name is null even though a name is being entered in the xaml. What mistake am i making.

- Забелешка:

Поради многуте фајлови и комплексноста на начинот на кодирање во uno platform, ChatGpt и DeepSeek не беа многу од помош при дебагирање. Грешките ги поправавме преку ConsoleLog на одредени параметри и нивно следење.

## **7\. Заклучок**

FitJournal е функционална апликација со лесен и интуитивен интерфејс за следење на тренинзи и вежби, која користи локално чување на податоци и работи крос-платформски. Ова ја прави погодна за корисници кои сакаат да го следат својот фитнес напредок без зависност од интернет.
