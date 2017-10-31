import pandas as pd
from pylab import rcParams
import seaborn as sns
import matplotlib.pyplot as plt

# отключим предупреждения Anaconda
import warnings

class Visualize:

    def __init__(self):
        # Python 2 and 3 compatibility
        # pip install future
        warnings.simplefilter('ignore')

        # увеличим дефолтный размер графиков
        rcParams['figure.figsize'] = 8, 5
        sns.set(style="darkgrid")

    def show_class_histogram(selfs, df):
        ax = df[["id", "class"]].groupby("class").count().plot(kind='bar', rot=45)
        total = df.shape[0]
        for p in ax.patches:
            height = p.get_height()
            ax.text(p.get_x() + p.get_width() / 2.,
                    height + 3,
                    height,
                    ha="center")
        plt.show()
