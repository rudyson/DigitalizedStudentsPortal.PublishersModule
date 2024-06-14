export interface ResponseWrapper<TModel> {
  id: string;
  data?: TModel;
  errors?: Record<string, string>;
  count?: number;
}
