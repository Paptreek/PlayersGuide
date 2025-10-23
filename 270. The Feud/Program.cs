using IField;
using McDroid;

Sheep sheep = new Sheep();
Cow cow = new Cow();

IField.Pig iFieldPig = new IField.Pig();
McDroid.Pig mcDroidPig = new McDroid.Pig();

namespace IField
{
    public class Sheep { }
    public class Pig { }
}

namespace McDroid
{
    public class Cow { }
    public class Pig { }
}