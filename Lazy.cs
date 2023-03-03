namespace Loxifi.LazyObjects
{
	/// <summary>
	/// Represents an object whos value is not calculated
	/// until it is requested
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Lazy<T>
	{
		private readonly Func<T?> _init;
		private T? _value;

		/// <summary>
		/// Constructs a new instance of this lazy object using the provided
		/// func for population
		/// </summary>
		/// <param name="init"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public Lazy(Func<T?> init)
		{
			this._init = init ?? throw new ArgumentNullException(nameof(init));
		}

		/// <summary>
		/// True if the init function has been called
		/// </summary>
		public bool IsLoaded { get; private set; }

		/// <summary>
		/// Returns the calculated underlying value,
		/// or calculates a new value based on the init
		/// </summary>
		public T? Value
		{
			get
			{
				if (!this.IsLoaded)
				{
					this.Init();
				}

				return this._value;
			}
		}

		/// <summary>
		/// Implicitly converts the func into a lazy object representing the result
		/// </summary>
		/// <param name="init"></param>
		public static implicit operator Lazy<T?>(Func<T?> init) => new(init);

		/// <summary>
		/// Returns the value of the object. Causes it to be loaded if it isn't
		/// </summary>
		/// <param name="lazy"></param>
		public static implicit operator T?(Lazy<T?> lazy) => lazy.Value;

		private void Init()
		{
			this._value = this._init();
			this.IsLoaded = true;
		}
	}
}