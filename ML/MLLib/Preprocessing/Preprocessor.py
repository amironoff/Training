import numpy
from numpy import random
from numpy import unique
import pandas


def categorical_to_numerical(feature_vector: numpy.array) -> numpy.array:
    """
    Converts a vector of categorical data into
    a numerical form, so  that it can be used
    in machine learning algorithms
    :rtype: numpy.array
    :param feature_vector: An N-dimensional categorical feature vector to convert
    :return: An N-dimensional numerical feature vector
    """
    features = numpy.unique(
        feature_vector, return_inverse=True)[1]

    return features


def one_hot_encode_categorical(feature_vector: pandas.Series) -> pandas.Series:
    """
       Converts a vector of categorical data into
       one-hot-encoded binary features, See Real-World Machine Learning, p. 141
       :rtype: pandas.DataFrame
       :param feature_vector: An N-dimensional categorical pandas.Series feature vector to convert
       :return: DataFrame with each category encoded as a binary feature vector
    """
    categories = unique(feature_vector)
    features = {}
    for cat in categories:
        binary = (feature_vector == cat)
        features["%s:%s" % (feature_vector.name, cat)] = binary.astype("int")

    return pandas.DataFrame(features)


def rescale_feature(feature_vector, f_min=-1, f_max=1):
    """
    Rescales a numerical feature vector
    to the requested range
    :param feature_vector: The numerical feature vector
    :param f_min: lower bound of the rescaled range
    :param f_max: upper bound of the rescaled range
    :return: A rescaled numerical feature vector along with
    the scaling factor
    """
    d_min, d_max = min(feature_vector), max(feature_vector)
    factor = (f_max - f_min) / (d_max - d_min)
    normalized = f_min + feature_vector * factor

    return normalized, factor


def split_dataframe_to_train_and_test(df, test_ratio=0.8):
    """
    Splits a pandas DataFrame into train and test set using the given ratio
    :param df: The pandas DataFrame to split
    :param test_ratio the portion of data to use for testing. The rest will form the training set.
    :return: training set and test set as separate DataFrame instances
    """
    M = len(df)
    rand_idx = numpy.arange(M)
    random.shuffle(rand_idx)
    train_idx = rand_idx[int(M * test_ratio):]
    test_idx = rand_idx[:int(M * test_ratio)]

    return df.ix[train_idx], df.ix[test_idx]


